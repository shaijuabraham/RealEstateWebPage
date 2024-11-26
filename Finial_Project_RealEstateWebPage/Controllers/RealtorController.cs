using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
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
        public IActionResult ViewRealtorSelectedPropertyInfo(string id)
        {

            Console.WriteLine("ViewRealtorSelectedPropertyInfo.");

            PropertyDataInfo propertyData = new PropertyDataInfo();
            AgentInfo agent = new AgentInfo();
            AgentCompanyInfo agentCompanyInfo = new AgentCompanyInfo();

            HomeInfo home = propertyData.GetHomeData(id);
            AgentInfo agentInfo = agent.AgentContactInfo(id);
            AgentCompanyInfo agentCompany = agentCompanyInfo.RelatorCompanyInfo(id);


            if (home != null)
            {
                var viewModel = new PropertyDetails
                {
                    HomeInfo = home,
                    AgentInfo = agentInfo,
                    AgentCompanyInfo = agentCompany,
                };

                return View("~/Views/RealtorPage/ViewRealtorProperty.cshtml", viewModel);
            }
            else
            {
                ViewBag.RelatorHome = null;
            }

            return View("~/Views/RealtorPage/RealtorMainPage.cshtml");
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            if (Request.Cookies["UserID"] != null)
            {
                Response.Cookies.Delete("UserID");
            }
            if (Request.Cookies["PropertyID"] != null)
            {
                Response.Cookies.Delete("PropertyID");
            }
            return RedirectToAction("Login", "Account");
        }


    }
}
