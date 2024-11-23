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
    }
}
