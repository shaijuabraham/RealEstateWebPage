using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
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

        [HttpPost]
        public IActionResult AcceptClientPropertyOffer(string PropertyID)
        {

            OfferClass offerClass = new OfferClass();
            offerClass.DeleteAcceptedOffer(PropertyID);
            return RedirectToAction("AgentViewOffer", "PropertyOffer");
        }

        [HttpPost]
        public IActionResult DeclineClientPropertyOffer(int id)
        {
            OfferClass offerClass = new OfferClass();
            offerClass.DeleteOffer(id);
            return RedirectToAction("AgentViewOffer", "PropertyOffer");

        }
    }
}
