using Elfie.Serialization;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Security.Cryptography.X509Certificates;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult AddLogin(string UserID, string Password)
        {
            Login login = new Login();
            login.UserID = UserID;
            login.Password = Password;
            bool loginStatus = login.User_Login();

            if (loginStatus == true)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddDays(1) };
                Response.Cookies.Append("UserID", UserID,options);
                ViewBag.ErrorMessage = "Login Accepted";
                return RedirectToAction("Index", "Realtor");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";

                return View("~/Views/Login&SignUp/LoginPage.cshtml");

            }
            //return View();
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Login&SignUp/LoginPage.cshtml");
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View("~/Views/Login&SignUp/SignUpPage.cshtml");
        }

        public IActionResult SignUpPage()
        {
            return View(new AccountRegistration());
        }


        [HttpPost]
        public IActionResult SignUp(AccountRegistration signUp) {

            string Username = signUp.UserID;

            signUp.UserAccountSignUp(Username, signUp.CompanyName, 
                                     signUp.CompanyStreet,
                                     signUp.CompanyCity, signUp.CompanyState, 
                                     signUp.CompanyZipCode,
                                     signUp.CompanyPhoneNumber, signUp.CompanyEmail, 
                                     signUp.LicenseNumber,
                                     signUp.BrokerType, signUp.Password);

            signUp.AccountPersonalInfoSignUp(Username, 
                                             signUp.FirstName,  
                                             signUp.LastName,
                                             signUp.Street, 
                                             signUp.City,
                                             signUp.State,
                                             signUp.ZipCode, 
                                             signUp.PhoneNumber,
                                             signUp.Email);

            signUp.AccountContactInfoSignUp(Username, 
                                            signUp.ContactFirstName, 
                                            signUp.ContactLastName,
                                            signUp.ContactStreet, 
                                            signUp.ContactCity, 
                                            signUp.ContactState,
                                            signUp.ContactZipCode, 
                                            signUp.ContactPhoneNumber, 
                                            signUp.ContactEmail);

            return View("~/Views/Login&SignUp/LoginPage.cshtml");
        }
    }
}
