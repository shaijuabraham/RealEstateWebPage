using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class AddPropertyInfo
    {

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        /*Add the property status to the database with propertyid*/
        public void AddPropertyStatus(string propertyID,
                                      DateTime date,
                                      string status)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPropertyStatus";
            objCommand.Parameters.Clear();

            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@ListingDate", date);
            objCommand.Parameters.AddWithValue("@PropertyStatus", status);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        /*methode to get the property status from database and put it in a string builder*/
        public string GetPropertyStatus(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyStatus";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objDB.DoUpdateUsingCmdObj(objCommand);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string status = row["PropertyStatus"].ToString();
                string date = Convert.ToDateTime(row["ListingDate"]).ToString("MM-dd-yyyy");
                return $"{status} (Last Updated : {date})";
            }
            else
            {
                return "No status record found for this property.";
            }
        }

        /*methode to update the propertystates based on the change made in  the managePage*/
        public void UpdatePropertyStatus(string propertyID,
                                     DateTime date,
                                     string status)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdatePropertyStatus";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@NewListingDate", date);
            objCommand.Parameters.AddWithValue("@NewPropertyStatus", status);
            objDB.DoUpdateUsingCmdObj(objCommand);

        }
    }
}
