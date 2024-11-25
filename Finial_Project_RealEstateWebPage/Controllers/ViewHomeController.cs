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

            PropertyDataInfo propertyData = new PropertyDataInfo();
            HomeInfo home = propertyData.GetHomeData(id);

            if (home != null)
            {
                if (home.HomeAmenities != null && home.HomeAmenities.SelectedAmenities.Count > 0)
                {
                     string.Join(", ", home.HomeAmenities.SelectedAmenities);
                }
                if (home.HomeUtility != null && home.HomeUtility.SelectedUtility.Count > 0)
                {
                    string.Join(", ", home.HomeUtility.SelectedUtility);
                }
                if (home.HomeRooms != null && home.HomeRooms.Count > 0)
                {
                    string.Join(", ", home.HomeRooms);
                }


                return View("~/Views/Home/ViewHome.cshtml",home);
            }
            else
            {
                ViewBag.RelatorHome = null; 
            }

            return View("~/Views/Home/HomePage.cshtml");
        }
    }
}
