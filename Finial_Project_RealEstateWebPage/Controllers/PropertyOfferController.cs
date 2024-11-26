using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class PropertyOfferController : Controller
    {
        public IActionResult AgentViewOffer()
        {
            Console.WriteLine("Agent offer Showing function Running .");
            string userId = Request.Cookies["UserID"];
            OfferClass home = new OfferClass();
            List<OfferClass> homes = home.GetPropertyOffer(userId);
            if (homes != null && homes.Count > 0)
            {
                ViewBag.RelatorProertyOffers = homes;
            }
            else
            {
                ViewBag.RelatorProertyOffers = new List<Home>();  //empty list to avoid null reference
            }

            return View("~/Views/RealtorPage/AgentOfferShowing.cshtml");
        }


        [HttpPost]
        public IActionResult MakePropertyOffer()
        {
            return View();  
        }
    }
}
