using System.Diagnostics;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult HomePageRun()
        {
            return View("~/Views/Home/HomePage.cshtml");
        }

    }
}
