using Elfie.Serialization;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public IActionResult AddLogin(Login login)
        {
            //Login login = new Login();
            
            bool loginStatus = login.User_Login();
            if (loginStatus == true)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddMinutes(2) };
                Response.Cookies.Append("UserID", login.UserID,options);
                //login.emialSent();               
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

        [HttpGet]
        public IActionResult PasswordReset()
        {
            return View("~/Views/PasswordReset/PasswordReset.cshtml");
        }

        [HttpPost]
        public IActionResult SignUp(string FirstName, string LastName,string Street,
                                    string City, string State, int ZipCode, string PhoneNumber,
                                    string Email, string ContactFirstName,string ContactLastName,
                                    string ContactStreet,string ContactCity,string ContactState, int ContactZipCode,
                                    string ContactPhoneNumber,string ContactEmail, string CompanyName, string CompanyStreet,
                                    string CompanyCity, string CompanyState,int CompanyZipCode, string CompanyPhoneNumber,
                                    string CompanyEmail, string Username, string Password, string LicenseNumber,string BrokerType,
                                     string question1, string answer1, string question2, string answer2, string question3, string answer3) {
            Question addQuestions = new Question();
                // Process each question and answer pair
                if (!string.IsNullOrEmpty(question1) && !string.IsNullOrEmpty(answer1))
                {
                    addQuestions.AddQuestionsAnswerFromSiginUpPage(Username, question1, answer1);
                }
                if (!string.IsNullOrEmpty(question2) && !string.IsNullOrEmpty(answer2))
                {
                    addQuestions.AddQuestionsAnswerFromSiginUpPage(Username, question2, answer2);
                }
                if (!string.IsNullOrEmpty(question3) && !string.IsNullOrEmpty(answer3))
                {
                    addQuestions.AddQuestionsAnswerFromSiginUpPage(Username, question3, answer3);
                }

            AccountRegistration signUp = new AccountRegistration();
            signUp.UserAccountSignUp(Username, CompanyName,
                                     CompanyStreet,
                                     CompanyCity, CompanyState,
                                     CompanyZipCode,
                                     CompanyPhoneNumber,CompanyEmail,
                                     LicenseNumber,
                                     BrokerType,Password);

            signUp.AccountPersonalInfoSignUp(Username,
                                             FirstName,
                                             LastName,
                                             Street,
                                             City,
                                             State,
                                             ZipCode,
                                             PhoneNumber,
                                             Email);

            signUp.AccountContactInfoSignUp(Username,
                                            ContactFirstName,
                                            ContactLastName,
                                            ContactStreet,
                                            ContactCity,
                                            ContactState,
                                            ContactZipCode,
                                            ContactPhoneNumber,
                                            ContactEmail);

            return View("~/Views/Login&SignUp/LoginPage.cshtml");
        }
    }
}
