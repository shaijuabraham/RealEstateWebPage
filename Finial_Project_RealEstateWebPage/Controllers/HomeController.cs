using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet]
        public IActionResult HomePageRun()
        {
            Home home = new Home();
            List<Home> homes = home.GetPartalHomedata();
            
            ViewBag.HomesList = homes; 
            return View("~/Views/Home/HomePage.cshtml");

        }
    }

}

