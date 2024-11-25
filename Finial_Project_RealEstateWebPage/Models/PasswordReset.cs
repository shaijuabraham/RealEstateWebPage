using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PasswordReset
    {

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*take the user input and retun if user name and passwrd it match
         else it will retun false*/
        public bool FindIfUserInDataBase(string UserID)
        {

            /*userID or Password is null or empty*/
            if (string.IsNullOrEmpty(UserID))
            {
                return false;  //invalid empty
            }

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetSecurityQuestions";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@UserID", UserID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string dbUserID = row["UserID"].ToString();
                if (dbUserID == UserID)
                {
                    return true;
                }
            }
            return false;
        }

        public string FindUsersEmail(string UserID)
        {
            // Check if UserID is null or empty
            if (string.IsNullOrEmpty(UserID))
            {
                return string.Empty;  // Invalid UserID
            }

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserEmailByID";  // Stored procedure to fetch email
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@UserID", UserID);

            // Get data from the database
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            // Check if the dataset contains any data
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Retrieve the email from the dataset
                DataRow row = ds.Tables[0].Rows[0];
                string dbEmail = row["Email"].ToString();  // Assuming column is named 'Email'
                return dbEmail;
            }

            // Return empty if no matching record is found
            return string.Empty;
        }
    }
}
