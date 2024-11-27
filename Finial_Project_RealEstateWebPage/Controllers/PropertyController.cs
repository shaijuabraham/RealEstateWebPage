using ClassLibrary;
using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;

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

            // Fetch price history using GetPriceHistory class
            try
            {
                GetPriceHistory getPriceHistory = new GetPriceHistory();
                string priceHistory = getPriceHistory.ShowPriceHistory(propertyID);
                ViewBag.PriceHistory = priceHistory;
            }
            catch (Exception ex)
            {
                ViewBag.PriceHistory = "An error occurred while fetching price history: " + ex.Message;
            }

            return View(propertyDetails);
        }




        // POST: Update Property Info
        [HttpPost]
        public IActionResult ManageProperty(PropertyDetails model, List<string> selectedAmenities, List<string> selectedUtilities)
        {
            if (model == null || model.HomeInfo == null)
            {
                TempData["Error"] = "Invalid data provided.";
                return RedirectToAction("ManageProperty", new { propertyID = model?.HomeInfo?.PropertyID });
            }

            try
            {
                // Update property info
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
                if (!string.IsNullOrEmpty(model.HomeInfo.PropertyID))
                {
                    manageProperty.UpdatePropertyListInfo(model.HomeInfo.PropertyID); // Clear existing lists first
                    manageProperty.AddAmenities(model.HomeInfo.PropertyID, selectedAmenities);
                    manageProperty.AddUtilities(model.HomeInfo.PropertyID, selectedUtilities);
                }

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

    }
}