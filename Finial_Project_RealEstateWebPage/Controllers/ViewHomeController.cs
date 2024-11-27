using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class ViewHomeController : Controller
    {
        [HttpGet]
        public IActionResult ViewPropertyInfo(string id)
        {

            Console.WriteLine("ViewPropertyInfo.");

            PropertyDataInfo propertyData = new PropertyDataInfo();
            AgentInfo agent = new AgentInfo();
            AgentCompanyInfo agentCompanyInfo = new AgentCompanyInfo();

            HomeInfo home = propertyData.GetHomeData(id);
            Home homedata = new Home();
            // DaysAvliabeOnMarket property in the home class
            //this will show the propety status and when  its last updated.
            homedata.DaysAvliabeOnMarket = homedata.GetpropertyDate(id);
            /*this methode to show the current ststus of the peroperty*/
            homedata.PropertyStatus = homedata.GetPropertyStatus(id);
            //pricehistory will show the property price -
            //history with the date to show when the chnages occured.
            homedata.PriceHistory = homedata.ShowPriceHistory(id);
            homedata.UserReview = homedata.GetPropertyReview(id);
            //this methode will show the agen inforionain based on the property.
            AgentInfo agentInfo = agent.AgentContactInfo(id);
            //this methode will show the agent company infromanation based on the propert.
            AgentCompanyInfo agentCompany = agentCompanyInfo.RelatorCompanyInfo(id);
            //if the home is not null the date will assigine to the PropertyDetails
            //class where its called each class and assigined the values.
            //so this will heples to call multiple @Model in the viewpage,
            //so its helps to call properties from multiple class.
            if (home != null)
            {
                // Create and populate view model 
                PropertyDetails viewModel = new PropertyDetails
                {
                    HomeInfo = home,
                    AgentInfo = agentInfo,
                    AgentCompanyInfo = agentCompany,
                    Home = homedata
                };
                return View("~/Views/Home/ViewHome.cshtml", viewModel);
            }
            else
            {
                ViewBag.RelatorHome = null; 
            }

            return View("~/Views/Home/HomePage.cshtml");
        }

        /*This will rediret to the Home Showing Page in the nav_bar*/
        public IActionResult SechduleHomeShowing(string id)
        {
            if (id != null)
            {
                Console.WriteLine($"{id}");
                var options = new CookieOptions { Expires = DateTime.Now.AddMinutes(10) };
                Response.Cookies.Append("PropertyID", id, options);
                return View("~/Views/Home/HomeShowingPage.cshtml");
            }
            return View();
        }

        [HttpPost]
        public IActionResult SechduleHomeShowingReq(string FullName,string PhoneNumber,
                    string Email, string Date, string time)
        {
            string Id = Request.Cookies["PropertyID"];
            Console.WriteLine(FullName, PhoneNumber, Email, Date, time);
            Console.WriteLine(Id);
            Console.WriteLine("startiing RequestPropertyShowing");
            if (Id != null) 
            {
                PropertyUserRequest propertyUserRequest = new PropertyUserRequest();
                propertyUserRequest.RequestPropertyShowing(Id,FullName,PhoneNumber,Email,Date,time);
                Response.Cookies.Delete("PropertyID");
                Console.WriteLine("Cookie deleted.");
               // return Re("ViewHome","SechduleHomeShowing");
            }

            return View("~/Views/Home/HomeShowingPage.cshtml");
        }




    }
}
