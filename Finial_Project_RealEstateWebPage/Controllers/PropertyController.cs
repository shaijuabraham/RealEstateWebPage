using ClassLibrary;
using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class PropertyController : Controller
    {
        private readonly ManageProperty manageProperty;

        public PropertyController()
        {
            manageProperty = new ManageProperty();
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Manage Property
        [HttpGet]
        public IActionResult ManageProperty(string propertyID)
        {
            if (string.IsNullOrEmpty(propertyID))
            {
                TempData["Error"] = "No property selected.";
                return RedirectToAction("Index", "Realtor");
            }

            var propertyData = new PropertyDataInfo();
            var home = propertyData.GetHomeData(propertyID);

            if (home == null)
            {
                TempData["Error"] = "Property data not found.";
                return RedirectToAction("Index", "Realtor");
            }

            var propertyDetails = new PropertyDetails
            {
                HomeInfo = home,
                AgentInfo = new AgentInfo().AgentContactInfo(propertyID),
                AgentCompanyInfo = new AgentCompanyInfo().RelatorCompanyInfo(propertyID)
            };

            // Fetch and add price history as a list
            var getPriceHistory = new GetPriceHistory();
            ViewBag.PriceHistory = getPriceHistory.GetPriceHistoryList(propertyID);

            return View(propertyDetails);

        }




        // POST: Update Property Info
        [HttpPost]
        public IActionResult ManageProperty(PropertyDetails model, List<string> selectedUtilities, List<string> selectedAmenities)
        {
            if (model == null || model.HomeInfo == null)
            {
                TempData["Error"] = "Invalid data provided.";
                return RedirectToAction("ManageProperty", new { propertyID = model?.HomeInfo?.PropertyID });
            }

            try
            {
                var propertyData = new PropertyDataInfo();
                var existingHome = propertyData.GetHomeData(model.HomeInfo.PropertyID);

                if (existingHome == null)
                {
                    TempData["Error"] = "Property data not found.";
                    return RedirectToAction("Index", "Realtor");
                }

                // Check if the asking price has changed
                if (existingHome.AskingPrice != model.HomeInfo.AskingPrice)
                {
                    var getPriceHistory = new GetPriceHistory();
                    getPriceHistory.AddPriceHistoryOffer(
                        model.HomeInfo.PropertyID,
                        DateTime.Now,
                        model.HomeInfo.AskingPrice
                    );
                }

                // Update property info
                var manageProperty = new ManageProperty();
                manageProperty.UpdatedatePropertyInfo(
                    model.HomeInfo.PropertyID,
                    model.HomeInfo.BuildingNumber,
                    model.HomeInfo.Street,
                    model.HomeInfo.City,
                    model.HomeInfo.State,
                    model.HomeInfo.ZipCode,
                    model.HomeInfo.PropertyType,
                    model.HomeInfo.YearBuilt,
                    model.HomeInfo.BedRooms,
                    model.HomeInfo.BathRooms,
                    model.HomeInfo.Heating,
                    model.HomeInfo.Cooling,
                    model.HomeInfo.AskingPrice,
                    int.Parse(model.HomeInfo.HomeSize),
                    model.HomeInfo.Description,
                    model.HomeInfo.Garage
                );

                // Update amenities and utilities
                manageProperty.UpdatePropertyListInfo(model.HomeInfo.PropertyID); // Clear existing data
                manageProperty.AddUtilities(model.HomeInfo.PropertyID, selectedUtilities);
                manageProperty.AddAmenities(model.HomeInfo.PropertyID, selectedAmenities);

                TempData["Success"] = "Property updated successfully!";
                return RedirectToAction("ManageProperty", new { propertyID = model.HomeInfo.PropertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("ManageProperty", new { propertyID = model.HomeInfo.PropertyID });
            }
        }





        // GET: Manage Home Rooms
        [HttpGet]
        public IActionResult ManageHomeRooms(string propertyID)
        {
            if (string.IsNullOrEmpty(propertyID))
            {
                TempData["Error"] = "No property selected.";
                return RedirectToAction("Index", "Realtor");
            }

            var propertyData = new PropertyDataInfo();
            var home = propertyData.GetHomeData(propertyID);

            if (home == null)
            {
                TempData["Error"] = "Property data not found.";
                return RedirectToAction("Index", "Realtor");
            }

            return View(home); // Pass HomeInfo model with rooms data to the view
        }

        // POST: Add Room
        [HttpPost]
        public IActionResult AddRoom(string propertyID, string roomName, int width, int length)
        {
            try
            {
                manageProperty.UpdatePropertyRoomsInfo(propertyID, roomName, width, length);
                TempData["Success"] = "Room added successfully!";
                return RedirectToAction("ManageHomeRooms", new { propertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("ManageHomeRooms", new { propertyID });
            }
        }

        // POST: Delete Room
        [HttpPost]
        public IActionResult DeleteRoom(int roomId, string propertyID)
        {
            try
            {
                manageProperty.DeleteRoominfo(roomId);
                TempData["Success"] = "Room deleted successfully!";
                return RedirectToAction("ManageHomeRooms", new { propertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred: " + ex.Message;
                return RedirectToAction("ManageHomeRooms", new { propertyID });
            }
        }


        // GET: Add Property
        [HttpGet]
        public IActionResult AddProperty()
        {
            return View("AddHome", new HomeInfo()); // Explicitly render AddHome.cshtml
        }


        // POST: Add Property
        [HttpPost]
        public IActionResult AddProperty(HomeInfo model, string PropertyStatus, string RoomName, int RoomWidth, int RoomLength, List<string> selectedAmenities, List<string> selectedUtilities)
        {
            if (!ModelState.IsValid)
            {
                // Debugging: Log validation errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                    }
                }

                TempData["Error"] = "Please fill all the required fields.";
                return View("AddHome", model); // Render the correct view explicitly
            }

            try
            {
                var propertyData = new PropertyDataInfo();

                // Generate Property ID based on the BuildingNumber
                if (string.IsNullOrEmpty(model.BuildingNumber))
                {
                    TempData["Error"] = "Building Number is required to generate Property ID.";
                    return View("AddHome", model);
                }

                string propertyID = propertyData.GeneratePropertyID(model.BuildingNumber);
                propertyStatus(propertyID, DateTime.Now, PropertyStatus);
                addRoomToDatabase(propertyID, RoomName, RoomWidth, RoomLength);

                // Save the property status using your existing AddPropertyInfo class
                var addPropertyInfo = new AddPropertyInfo();
                addPropertyInfo.AddPropertyStatus(propertyID, DateTime.Now, propertyStatus);

                // Save the property information
                propertyData.AddProperty(new HomeInfo
                {
                    PropertyID = propertyID,
                    BuildingNumber = model.BuildingNumber,
                    Street = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    PropertyType = model.PropertyType,
                    YearBuilt = model.YearBuilt,
                    BedRooms = model.BedRooms,
                    BathRooms = model.BathRooms,
                    Heating = model.Heating,
                    Cooling = model.Cooling,
                    AskingPrice = model.AskingPrice,
                    Description = model.Description,
                    Garage = model.Garage
                });

                // Save selected utilities and amenities
                propertyData.AddUtilities(new HomeInfo { PropertyID = propertyID, HomeUtility = new Utility(selectedUtilities) });
                propertyData.AddAmenities(new HomeInfo { PropertyID = propertyID, HomeAmenities = new Amenities(selectedAmenities) });

                TempData["Success"] = "Property added successfully!";
                return RedirectToAction("Index", "Realtor");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View("AddHome", model);
            }
        }






    }
}