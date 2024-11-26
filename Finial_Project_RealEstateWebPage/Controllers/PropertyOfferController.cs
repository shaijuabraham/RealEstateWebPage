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
                ViewBag.RelatorProertyOffers = new List<Home>();  //avoid null reference
            }

            return View("~/Views/RealtorPage/AgentOfferShowing.cshtml");
        }


        public IActionResult MakePropertyOffer(OfferClass offerClass)
        {
            string propertyID = offerClass.PropertyID;
            string fullName = offerClass.FullName;
            string buyerPhone = offerClass.BuyerPhone;
            string email = offerClass.BuyerEmail;
            string offerAmount = offerClass.OfferAmount;
            string saleType = offerClass.SaleType;
            string contingencies = offerClass.Contingencies;
            string needToSell = offerClass.NeedToSell;
            string moveInDate = offerClass.MoveInDate;
            offerClass.MakeOffer(propertyID, fullName,buyerPhone, email,
                                 offerAmount,saleType,
                                 contingencies, needToSell, moveInDate);
            return View("~/Views/Home/UserMakeOffer.cshtml");  
        }
    }
}
