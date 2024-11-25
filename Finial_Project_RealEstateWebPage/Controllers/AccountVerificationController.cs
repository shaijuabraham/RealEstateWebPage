using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AccountVerificationController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult VerifySecurityQuestion1(string AnswerSecurityQuestion1, string AnswerSecurityQuestion2, string AnswerSecurityQuestion3)
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyUserID(Login login)
        {
            PasswordReset passwordReset = new PasswordReset();
            bool findUser = passwordReset.FindIfUserInDataBase(login.UserID);
            if (findUser == true)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
                Response.Cookies.Append("UserID", login.UserID, options);
                return View("~/Views/PasswordReset/SecurityQuestions.cshtml");
            }
            return View();
        }

    }
}
