using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
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
                ViewBag.ErrorMessage = "Login Accepted";
                return View("~/Views/Login&SignUp/LoginPage.cshtml");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid username or password.";

                return View("~/Views/Login&SignUp/LoginPage.cshtml");

            }
            //return View();
            //Hsbsbhwbihbqhiqibhibabcbhachakaakbakcckas
            //New way commit
            //New way commit
            //New way commit
            //New way commit
            //New way commit
            //New way commit
        }
        //New way commit
        //New way commit

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


        [HttpPost]
        public IActionResult SignUp(string BrokerType) { 
        

          AccountRegistration signUp = new AccountRegistration();

            signUp.UserAccountSignUp(signUp.UserID, signUp.CompanyName, 
                                     signUp.CompanyStreet,
                                     signUp.CompanyCity, signUp.CompanyState, 
                                     signUp.CompanyZipCode,
                                     signUp.CompanyPhoneNumber, signUp.CompanyEmail, 
                                     signUp.LicenseNumber,
                                     BrokerType, signUp.Password);

            signUp.AccountPersonalInfoSignUp(signUp.UserID, 
                                             signUp.FirstName,  
                                             signUp.LastName,
                                             signUp.Street, 
                                             signUp.City,
                                             signUp.State,
                                             signUp.ZipCode, 
                                             signUp.PhoneNumber, signUp.Email);

            signUp.AccountContactInfoSignUp(signUp.UserID, signUp.ContactFirstName, 
                                            signUp.ContactLastName,
                                            signUp.ContactStreet, signUp.ContactCity, 
                                            signUp.ContactState,
                                            signUp.ContactZipCode, signUp.ContactPhoneNumber, 
                                            signUp.ContactEmail);

            return View("~/Views/Login&SignUp/SignUpPage.cshtml");
        }
    }
}
