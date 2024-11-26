using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AgentPropertyShowingController : Controller
    {
        public IActionResult AgentHomeShowing()
        {
            Console.WriteLine("Agent Home Showing .");
            string userId = Request.Cookies["UserID"];
            PropertyShowing home = new PropertyShowing();
            List<PropertyShowing> homes = home.PropertyDataInfo(userId);
            if (homes != null && homes.Count > 0)
            {
                ViewBag.RelatorHomeShowing = homes;
            }
            else
            {
                ViewBag.RelatorHomeShowing = new List<Home>();  //empty list to avoid null reference
            }

            return View("~/Views/RealtorPage/HomeViewing.cshtml");

        }
    }
}
