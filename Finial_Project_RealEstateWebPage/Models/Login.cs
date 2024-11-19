using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class Login
    {
        private string userID;
        private string password;


        public Login(){}

        public String UserID { 
            get { return userID; }
            set { userID = value; }
        }
        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*take the user input and retun if user name and passwrd it match
         else it will retun false*/
        public bool User_Login()
        {

            /*userID or Password is null or empty*/
            if (string.IsNullOrEmpty(this.UserID) || string.IsNullOrEmpty(this.Password))
            {
                return false;  //invalid empty
            }

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetLoginInfo";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@UserID", this.UserID);
            objCommand.Parameters.AddWithValue("@Password", this.Password);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string dbUserID = row["UserID"].ToString();
                string dbPassword = row["Password"].ToString();
                if (dbUserID == this.UserID && dbPassword == this.Password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
