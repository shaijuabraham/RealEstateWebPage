using ClassLibrary;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AddHomeController : Controller
    {
        public IActionResult AddProperty()
        {
            return View("~/Views/Home/AddHome.cshtml");
        }

        [HttpPost]
        public IActionResult AddPropertyInfo(HomeInfo homeinfo, string PropertyStatus, string HomeType,
                                             string Heating, string Cooling,string Garage,string Description)
        {

            AddPropertyImage addPropertyImage = new AddPropertyImage();
            addPropertyImage.addpropertyDate(propertyID, date);
            GetPriceHistory getPriceHistory = new GetPriceHistory();
            getPriceHistory.AddPriceHistoryOffer(propertyID, date, askingPrice);

            return View();
        }
    }
}
