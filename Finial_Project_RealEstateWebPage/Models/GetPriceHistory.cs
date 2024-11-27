using System.Data.SqlClient;
using System.Data;
using System.Text;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class GetPriceHistory
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        /*When user change the  price in the Manage page
         the methde will take the taje that vale with datatime and propery id 
        will add it in the */
        public void AddPriceHistoryOffer(string propertyId, DateTime date, decimal price)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPropertyPriceHistory";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyId);
            objCommand.Parameters.AddWithValue("@Price", price);
            objCommand.Parameters.AddWithValue("@Date", date);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*this methode show the property pricehistory on Viewhome page 
         when user select teh perioerty by retun it ina string */
        public string ShowPriceHistory(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyPriceHistory";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder priceHistory = new StringBuilder();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string price = Convert.ToDecimal(row["Price"]).ToString("C");
                    string date = Convert.ToDateTime(row["Date"]).ToString("MM-dd-yyyy");
                    priceHistory.AppendLine($"{date}:  {price} <br/>");
                }
                return priceHistory.ToString();
            }
            else
            {
                return "No price change for this property.";
            }
        }

        public List<string> GetPriceHistoryList(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyPriceHistory";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);

            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            List<string> priceHistory = new List<string>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string price = Convert.ToDecimal(row["Price"]).ToString("C");
                    string date = Convert.ToDateTime(row["Date"]).ToString("MM-dd-yyyy");
                    priceHistory.Add($"{date}: {price}");
                }
            }

            return priceHistory;
        }

    }
}
