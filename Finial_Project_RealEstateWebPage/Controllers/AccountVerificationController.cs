using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AccountVerificationController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult VerifySecurityQuestion(string AnswerSecurityQuestion1, string AnswerSecurityQuestion2, string AnswerSecurityQuestion3)
        {
            return View();
        }

        public IActionResult GetSecurityQuestions()
        {


            return View("~/Views/PasswordReset/SecurityQuestions.cshtml");
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
                return RedirectToAction("GetSecurityQuestions", "AccountVerification");
                //return View("~/Views/PasswordReset/SecurityQuestions.cshtml");
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            List<Question> question = new List<Question>();

            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand();
                string userId = Request.Cookies["UserID"];
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetSecurityQuestions";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("UserID", userId);

                DataSet ds = objDB.GetDataSetUsingCmdObj(command);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int totalRows = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < totalRows; i++)
                    {
                        DataRow record = ds.Tables[0].Rows[i];
                        Question ques = new Question();

                        if (!record.IsNull("Question"))
                            ques.Question1 = record["Question"].ToString();
                        

                        question.Add(ques);
                    }
                }
                else
                {
                    Console.WriteLine("No data available in the result set.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }

            ViewBag.QuestionList = question;
            return View("~/Views/Home/HomePage.cshtml",question);

        }
    }
}
