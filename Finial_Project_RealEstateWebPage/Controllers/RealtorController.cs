using Microsoft.AspNetCore.Mvc;
using System.Drawing.Text;
using System.Security.Cryptography.Pkcs;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class RealtorController : Controller
    {
        public IActionResult Index()
        {
            string userId = Request.Cookies["UserID"];

            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.Message = $"Welcome back, {userId}!";



            }
            else
            {
                ViewBag.Message = "User is not logged in.";
            }
            return View();
        }
    }
}
