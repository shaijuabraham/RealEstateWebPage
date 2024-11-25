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
            AgentInfo agentInfo = agent.AgentContactInfo(id);
            AgentCompanyInfo agentCompany = agentCompanyInfo.RelatorCompanyInfo(id);

            if (home != null)
            {
                // Create and populate view model 
                PropertyDetails viewModel = new PropertyDetails
                {
                    HomeInfo = home,
                    AgentInfo = agentInfo,
                    AgentCompanyInfo = agentCompany
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
