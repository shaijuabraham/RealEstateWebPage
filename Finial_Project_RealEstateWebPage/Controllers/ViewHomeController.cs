using Finial_Project_RealEstateWebPage.Models;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using Microsoft.AspNetCore.Mvc;

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
                /*if (home.HomeAmenities != null && home.HomeAmenities.SelectedAmenities.Count > 0)
                {
                     string.Join(", ", home.HomeAmenities.SelectedAmenities);
                }
                if (home.HomeUtility != null && home.HomeUtility.SelectedUtility.Count > 0)
                {
                    string.Join(", ", home.HomeUtility.SelectedUtility);
                }*/

                // Create and populate view model
                var viewModel = new PropertyDetails
                {
                    HomeInfo = home,
                    AgentInfo = agentInfo,
                    AgentCompanyInfo = agentCompany,
                };


                return View("~/Views/Home/ViewHome.cshtml", viewModel);
            }
            else
            {
                ViewBag.RelatorHome = null; 
            }

            return View("~/Views/Home/HomePage.cshtml");
        }

/*this is the For teh home Shoing NAv Bar */
        public IActionResult SechduleHomeShowing(string id)
        {
            
            return View();
        }
    }
}
