using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models.associateclass
{
    public class OfferClass
    {
        public string OfferAmount { get; set; }
        public string SaleType { get; set; }
        public string Contingencies { get; set; }
        public string NeedToSell { get; set; }
        public string MoveInDate { get; set; }
        public string FullName { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerEmail { get; set; }
        public string PropertyID { get; set; }
        public string OfferId { get; set; }



        public OfferClass() { }//empty Constructor
        public OfferClass(string offerAmount, string saleType, string contingencies, string needToSell, 
                           string moveInDate, string fullName, string buyerPhone, string buyerEmail)
        {
            OfferAmount = offerAmount;
            SaleType = saleType;
            Contingencies = contingencies;
            NeedToSell = needToSell;
            MoveInDate = moveInDate;
            FullName = fullName;
            BuyerPhone = buyerPhone;
            BuyerEmail = buyerEmail;
        }

        /*Get the list of property offcer baseed on the agentID*/

        public List<OfferClass> GetPropertyOffer(string userId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            Console.WriteLine("Available in the result set.1");
            List<OfferClass> homes = new List<OfferClass>();
            try
            {

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetPropertyOffer";
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@AgentID", userId);
                DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow propertyRow in ds.Tables[0].Rows)
                    {
                        OfferClass home = new OfferClass();

                        if (propertyRow.Table.Rows.Count > 0)
                        {
                            home.PropertyID = propertyRow["PropertyId"].ToString();
                            home.OfferId = propertyRow["id"].ToString();
                        }

                        homes.Add(home);
                        Console.WriteLine("Available in the result set.2");
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

            return homes;

        }
    }
}
