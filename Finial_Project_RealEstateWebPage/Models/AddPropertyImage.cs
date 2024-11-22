using System.Data.SqlClient;
using System.Data;
using System.Text;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class AddPropertyImage
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

        /*Methode to get the image from user andd add it on property*/
        //-- blank it to add the Image Methode 


        /*Get the property delatils for the home shoing request from the user*/
        public void RequestPropertyShowing(string propertyID, string name, string phonNumbaer, string email, string date, string time)
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
        /*Add the makeoffer informanation to the database*/
        public void MakeOffer(string propertyID, string fullName, string buyerPhone, string email,
                                string offerAmount, string saleType,
                                string contingencies, string needToSell, string moveInDate)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertOfferRequest";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            objCommand.Parameters.AddWithValue("@FullName", fullName);
            objCommand.Parameters.AddWithValue("@OfferAmount", offerAmount);
            objCommand.Parameters.AddWithValue("@SaleType", saleType);
            objCommand.Parameters.AddWithValue("@Contingencies", contingencies);
            objCommand.Parameters.AddWithValue("@NeedToSell", needToSell);
            objCommand.Parameters.AddWithValue("@MoveInDate", moveInDate);
            objCommand.Parameters.AddWithValue("@BuyerPhone", buyerPhone);
            objCommand.Parameters.AddWithValue("@BuyerEmail", email);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        /*methode to get the property date when its addd to the database*/
        public void addpropertyDate(string propertyID, DateTime moveInDate)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAddedDate";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            objCommand.Parameters.AddWithValue("@dateAdded", moveInDate);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        public string GetpropertyDate(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAddedDate";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder addeddate = new StringBuilder();
                foreach (DataRow row in ds.Tables[0].Rows)
                {/*add the date to the date vale from the selected row*/
                    DateTime date = Convert.ToDateTime(row["dateAdded"]);
                    addeddate.AppendLine($"Days On Market: {GetpropertyaddedDate(date)} <br/>");
                }
                return addeddate.ToString();
            }
            else
            {
                return "Days ON Market : Not Avliable";
            }
        }

        public int GetpropertyaddedDate(DateTime date)
        {
            DateTime dateTime = DateTime.Now;
            /*user time span to way to store the difference between days*/
            TimeSpan difference = dateTime - date;
            int days = difference.Days;
            return days;
        }
    }
}
