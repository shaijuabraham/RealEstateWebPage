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
        string webApiUrl = "https://localhost:44397/swagger/index.html";
        //run the Addproperty IAction methode Wuth the page load.
        public IActionResult AddProperty(HomeInfo addHome)
        {
           //String webApi = ""
            string userId = Request.Cookies["UserID"];

            ViewBag.UserID = userId;
            return View("~/Views/RealtorPage/AddHome.cshtml");
        }
    }
}
