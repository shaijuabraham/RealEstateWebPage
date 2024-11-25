using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Cryptography.Pkcs;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class RealtorController : Controller
    {

       [HttpGet]
        public IActionResult Index()
        {

            Console.WriteLine("indexPage.");
            string userId = Request.Cookies["UserID"];
            Home home = new Home();
            List<Home> homes = home.GetPartalHomedata(userId);
            if (homes != null && homes.Count > 0)
            {
                ViewBag.RelatorHomesList = homes;  
            }
            else
            {
                ViewBag.RelatorHomesList = new List<Home>();  //empty list to avoid null reference
            }

            return View("~/Views/RealtorPage/RealtorMainPage.cshtml");
        }

        public IActionResult Index2()
        {
            return View("~/Views/RealtorPage/RealtorMainPage.cshtml");
        }

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


                    return View("~/Views/Home/ViewRealtorProperty.cshtml", home);
                }
                else
                {
                    ViewBag.RelatorHome = null;
                }

                return View("~/Views/Home/HomePage.cshtml");
            }

    }
}
