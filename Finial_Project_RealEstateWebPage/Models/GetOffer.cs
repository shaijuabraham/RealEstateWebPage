using FunctionClassLibrary.AssociateClass;
using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class GetOffer
    {

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*Get the list of property id based on the AgentId*/
        public DataSet GetPropertyOffer(string userId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyOffer";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@AgentID", userId);
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        /*Get the property oferdetails based on the id and show the soecfic 
         * property selected from the user and  return to the OfferClass.*/
        public DataSet ShowPropertyOfferDetails(string id)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "ShowPropertyOfferDetails";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Id", id);
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        public OfferClass GetOfferData(string propertyID)
        {
            DataSet propertyData = ShowPropertyOfferDetails(propertyID);
            if (propertyData.Tables.Count == 0 || propertyData.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            DataRow propertyRow = propertyData.Tables[0].Rows[0];
            string fullName = propertyRow["FullName"].ToString();
            string offerAmount = propertyRow["OfferAmount"].ToString();
            string saleType = propertyRow["SaleType"].ToString();
            string contingencies = propertyRow["Contingencies"].ToString();
            string moveInDate = propertyRow["MoveInDate"].ToString();
            string needToSell = propertyRow["NeedToSell"].ToString();
            string buyerPhone = propertyRow["BuyerPhone"].ToString();
            string buyerEmail = propertyRow["BuyerEmail"].ToString();
            OfferClass Offer = new OfferClass(offerAmount, saleType, contingencies, needToSell,
                                                moveInDate, fullName, buyerPhone, buyerEmail);
            return Offer;
        }

        /*methode to delete the specfic property id data based on the 
         user selction using when usere decline the specfic offer*/
        public void DeleteOffer(string OfferId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeletePropertyOfferDetails";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Id", OfferId);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*when user accept a offer its delete the that offfer and all the 
         * property data based on the propertyid*/
        public void DeleteAcceptedOffer(string propertyId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteAcceptPropertyOffer";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyId);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*Delete the data from the data based baded on selected property*/
        public void DeleteProperty(string propertyId)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteProperty";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyId);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
    }
}
