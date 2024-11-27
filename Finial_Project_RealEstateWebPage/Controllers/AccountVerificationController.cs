﻿using Finial_Project_RealEstateWebPage.Models;
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
/*
        [HttpPost]
        public IActionResult VerifySecurityQuestion(string AnswerSecurityQuestion1, string AnswerSecurityQuestion2, string AnswerSecurityQuestion3)
        {
            return View();
        }*/

        public IActionResult GetSecurityQuestions()
        {
            string userId = Request.Cookies["PasswordRestUserID"];

            return View("~/Views/PasswordReset/SecurityQuestions.cshtml");
        }

        [HttpPost]
        public IActionResult VerifyUserID(Login login)
        {
            PasswordReset passwordReset = new PasswordReset();
            bool findUser = passwordReset.FindIfUserInDataBase(login.UserID);
            if (findUser == true)
            {
                var options = new CookieOptions { Expires = DateTime.Now.AddHours(1) };
                Response.Cookies.Append("PasswordRestUserID", login.UserID, options);
                return RedirectToAction("GetQuestions", "AccountVerification");
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
                string userId = Request.Cookies["PasswordRestUserID"];
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
                            ques.Questions = record["Question"].ToString();
                            ques.QuestionId = int.Parse(record["id"].ToString());
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
            return View("~/Views/PasswordReset/SecurityQuestions.cshtml");

        }

        [HttpPost]
        public IActionResult VerifyUser(List<Question> Questions)
        {
            if (Questions == null || Questions.Count == 0)
            {
                ViewBag.ErrorMessage = "No questions were answered. Please try again.";
                return View();
            }

            Question questionModel = new Question();

            foreach (var questionItem in Questions)
            {
                bool isValid = questionModel.GetQuestionsAnswer(questionItem.QuestionId, questionItem.Questions, questionItem.Answer);
                if (isValid == true)
                {
                    ViewBag.SuccessMessage = "All answers are correct!";
                    // Redirect to the next step or perform further actions
                    return View("~/Views/Login&SignUp/LoginPage.cshtml");
                }
                else
                {
                    ViewBag.ErrorMessage = $"Invalid answer for Question ID: {questionItem.QuestionId}";
                    break; // Stop checking further if one is invalid
                    
                }
            }
            return View("~/Views/PasswordReset/SecurityQuestions.cshtml");
        }

    }
}
