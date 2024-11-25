using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class ViewHomeController : Controller
    {
        [HttpGet]
        public IActionResult ViewPropertyInfo(string id)
        {

            Console.WriteLine("ViewPropertyInfo.");

            //PropertyData propertyData = new PropertyData();
            HomeInfo home = propertyData.GetHomeData(id); 

            if (home != null)
            {
                ViewBag.RelatorHome = home; // Pass the single object
            }
            else
            {
                ViewBag.RelatorHome = null; // Handle null case
            }

            return View("~/Views/RealtorPage/RealtorMainPage.cshtml");
        }
    }
}
