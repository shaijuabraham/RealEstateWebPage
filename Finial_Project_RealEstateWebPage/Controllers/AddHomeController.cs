using ClassLibrary;
using Finial_Project_RealEstateWebPage.Models;
using FunctionClassLibrary.AssociateClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mono.TextTemplating;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AddHomeController : Controller
    {
        public IActionResult AddProperty()
        {
            return View("~/Views/RealtorPage/AddHome.cshtml");
        }

        [HttpPost]
        public IActionResult AddPropertyInfo(HomeInfo homeinfo)
        {
            if (ModelState.IsValid)
            {
                PropertyInfoData propertyInfo = new PropertyInfoData();
                PropertyData propertyData = new PropertyData();
                string propertyID = propertyInfo.GeneratePropertyID(homeinfo.BuildingNumber);
                string status = Request.Form["PropertyStatus"];
                DateTime date = DateTime.Now;
 
        /*        //get the amenities from the add home. 
                List<string> amenities = new List<string>();
                amenities = homeinfo.HomeAmenities.SelectedAmenities;
                // get the utilities 
                List<string> utilities = new List<string>();
                utilities = homeinfo.HomeUtility.SelectedUtility;*/


                AddPropertyImage addPropertyImage = new AddPropertyImage();
                addPropertyImage.addpropertyDate(propertyID, date);
                GetPriceHistory getPriceHistory = new GetPriceHistory();
                getPriceHistory.AddPriceHistoryOffer(propertyID, date, homeinfo.AskingPrice);
                propertyInfo.AddProperty(homeinfo);// This method handles property insertion
                propertyInfo.AddUtilities(homeinfo); // This method handles utility insertion
                propertyInfo.AddAmenities(homeinfo); // This method handles amenities insertion
                propertyInfo.AddPropertyRoomsInfo(homeinfo);//  add the rooms data
                propertyStatus(propertyID, date, status);//add the property status  

                return View();
            }
            else
            {
                ViewBag.ErrorMessage = $"Invalid.{homeinfo}";
            }
            return View();


        }


        private void propertyStatus(string propertyID, DateTime date, string status)
        {
            AddPropertyInfo addPropertyInfo = new AddPropertyInfo();
            addPropertyInfo.AddPropertyStatus(propertyID, date, status);
        }
    }
}
