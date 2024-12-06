using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System.Data;
using System.Data.SqlClient;
using Utilities;

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

            return View("AgentOfferShowing");
        }

        public IActionResult MakePropertyOffer(string id)
        {
            if (id != null)
            {
                Console.WriteLine($"{id}");
                var options = new CookieOptions { Expires = DateTime.Now.AddMinutes(10) };
                Response.Cookies.Append("SelectedPropertyID", id, options);
                return View("UserMakeOffer");
            }
            return View();
        }

        [HttpPost]
        public IActionResult MakePropertyOfferReq(string FullName, string BuyerPhone,
                                                string BuyerEmail, string OfferAmount, string SaleType,
                                                string Contingencies, string NeedToSell, string MoveInDate)
        {

            string propertyID = Request.Cookies["SelectedPropertyID"]; 

            //if (propertyID != null && offerClass != null)
            if (propertyID != null)
            {
                OfferClass offerClass = new OfferClass();
                offerClass.MakeOffer(propertyID, FullName, BuyerPhone, BuyerEmail,
                                                OfferAmount, SaleType,
                                                Contingencies, NeedToSell, MoveInDate);
                return RedirectToAction("ViewPropertyInfo", "ViewHome", new { id = propertyID });
            }
            else
            {
                Console.WriteLine("Having error to sent request plese refresh the page");
                ViewBag.ErrorMessage = "Having error to sent request plese refresh the page";
                 
                return View("UserMakeOffer");
            }

           // return RedirectToAction("ViewPropertyInfo", "ViewHome", new { id = propertyID });

        }
        /*Delete the property Showing request*/
        [HttpPost]
        public IActionResult DeleteHomeSowingRequest(int id)
        {
            if (id > 0)
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeletePropertyShowing"
                };
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@ID", id);

                objDB.DoUpdate(command);
            }
            return RedirectToAction("AgentHomeShowing", "AgentPropertyShowing");
         
        }


    }
}
