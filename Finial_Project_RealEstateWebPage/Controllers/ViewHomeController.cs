﻿using Finial_Project_RealEstateWebPage.Models;
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
            HomeInfo home = propertyData.GetHomeData(id);

            if (home != null)
            {
                return View("~/Views/Home/ViewHome.cshtml",home);
            }
            else
            {
                ViewBag.RelatorHome = null; // Handle null case
            }

            return View("~/Views/Home/HomePage.cshtml");
        }
    }
}
