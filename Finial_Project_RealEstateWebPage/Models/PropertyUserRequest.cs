using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PropertyUserRequest
    {


        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        /*Get the property delatils for the home shoing request from the user*/
        public void RequestPropertyShowing(string propertyID, string name, 
                                string phonNumbaer, string email, string date, string time)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPropertyShowingRequest";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            objCommand.Parameters.AddWithValue("@FullName", name);
            objCommand.Parameters.AddWithValue("@Email", email);
            objCommand.Parameters.AddWithValue("@PhoneNumber", phonNumbaer);
            objCommand.Parameters.AddWithValue("@Date", date);
            objCommand.Parameters.AddWithValue("@Time", time);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
