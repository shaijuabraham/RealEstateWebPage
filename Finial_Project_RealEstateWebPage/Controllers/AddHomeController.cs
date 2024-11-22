using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AddHomeController : Controller
    {
        public IActionResult AddProperty()
        {
            return View("~/Views/Home/AddHome.cshtml");
        }
    }
}
