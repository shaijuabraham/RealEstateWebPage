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

        [HttpGet]
        public IActionResult ManageProperty(string PropertyID)
        {
            if (string.IsNullOrEmpty(PropertyID))
            {
                TempData["Error"] = "No property selected.";
                return RedirectToAction("Index", "Realtor");
            }

            Console.WriteLine($"PropertyID received: {PropertyID}");

            var propertyData = new PropertyDataInfo();
            var home = propertyData.GetHomeData(PropertyID);

            if (home == null)
            {
                TempData["Error"] = "Property data not found.";
                return RedirectToAction("Index", "Realtor");
            }

            var propertyDetails = new PropertyDetails
            {
                HomeInfo = home,
                AgentInfo = new AgentInfo().AgentContactInfo(PropertyID),
                AgentCompanyInfo = new AgentCompanyInfo().RelatorCompanyInfo(PropertyID)
            };

            return View(propertyDetails);
        }


        [HttpPost]
        public IActionResult UpdateProperty(PropertyDetails model, List<string> selectedUtilities, List<string> selectedAmenities)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Invalid data. Please check the form and try again.";
                return View("ManageProperty", model); // Return to the same view
            }

            try
            {
                // Log values for debugging
                Console.WriteLine($"Updating Property: {model.HomeInfo.PropertyID}");
                Console.WriteLine($"Building Number: {model.HomeInfo.BuildingNumber}");
                Console.WriteLine($"Street: {model.HomeInfo.Street}");
                Console.WriteLine($"City: {model.HomeInfo.City}");
                // Add similar logs for other fields...

                // Update property details
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
                    int.Parse(model.HomeInfo.HomeSize), // Ensure HomeSize is an integer
                    model.HomeInfo.Description,
                    model.HomeInfo.Garage
                );

                // Update amenities and utilities
                manageProperty.UpdatePropertyListInfo(model.HomeInfo.PropertyID); // Clear existing data
                manageProperty.AddUtilities(model.HomeInfo.PropertyID, selectedUtilities);
                manageProperty.AddAmenities(model.HomeInfo.PropertyID, selectedAmenities);

                TempData["Success"] = "Property updated successfully!";
                return RedirectToAction("ManageProperty", new { PropertyID = model.HomeInfo.PropertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return View("ManageProperty", model);
            }
        }


        [HttpPost]
        public IActionResult AddRoom(string PropertyID, string roomName, int width, int length)
        {
            try
            {
                manageProperty.UpdatePropertyRoomsInfo(PropertyID, roomName, width, length);
                TempData["Success"] = "Room added successfully!";
                return RedirectToAction("ManageProperty", new { PropertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("ManageProperty", new { PropertyID });
            }
        }

        [HttpPost]
        public IActionResult DeleteRoom(int roomId, string PropertyID)
        {
            try
            {
                manageProperty.DeleteRoominfo(roomId);
                TempData["Success"] = "Room deleted successfully!";
                return RedirectToAction("ManageProperty", new { PropertyID });
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("ManageProperty", new { PropertyID });
            }
        }
    }
}
