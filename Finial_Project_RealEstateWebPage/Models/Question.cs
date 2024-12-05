using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Utilities;
using System.Data.Common;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Questions {  get; set; }
        public string Answer {  get; set; } 



        public Question()
        {

        }

        public bool GetQuestionsAnswer(int questionId, string questions, string answers)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetSecurityQuestionsAnswers" 
                };
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@id", questionId);  
                command.Parameters.AddWithValue("@Question", questions);
                command.Parameters.AddWithValue("@Answer", answers);

                DataSet ds = objDB.GetDataSetUsingCmdObj(command);

                // If a matching row exists, return true
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow record = ds.Tables[0].Rows[0]; 
                    int ID = int.Parse(record["id"].ToString());
                    string questionOne = record["Question"].ToString();
                    string answer = record["Answer"].ToString();

                    // Compare values (use case-insensitive comparison for strings if necessary)
                    if (ID == questionId && questionOne == questions && answer == answers)
                    {
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("No matching question and answer found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }

            return false;
        }


        public bool PasswordRest(string password, string userid)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            /*userID or Password is null or empty*/
            if (string.IsNullOrEmpty(password))
            {
                return false;  //invalid empty
            }

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdatePassword";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@UserID", userid);
            objCommand.Parameters.AddWithValue("@Password", password);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string dbUserID = row["UserID"].ToString();
                string dbPassword = row["Password"].ToString();
                if (dbUserID == userid && dbPassword == password)
                {
                    return true;
                }
            }
            return false;
        }




        public void AddQuestionsAnswerFromSiginUpPage(string userID, string questions, string answers)
        {
            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddQuestionsAnswerFromSiginUpPage"
                };
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userID);
                command.Parameters.AddWithValue("@Question", questions);
                command.Parameters.AddWithValue("@Answer", answers);
                objDB.DoUpdate(command);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error retrieving data: " + ex.Message);
            }
        }
    }
}
