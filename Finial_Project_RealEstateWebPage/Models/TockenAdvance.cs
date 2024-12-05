using Mono.TextTemplating;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json.Serialization;
using Utilities;
using System.Runtime.Serialization.Formatters.Binary;

namespace Finial_Project_RealEstateWebPage.Models
{

    [Serializable]
    public class TockenAdvance
    {

        public string AccountNumber { get; set; }

        public string RoutingNumber { get; set; }

        public float Amount { get; set; }


        public void SentTockenAmountForSelectProperty(string propertyId,float amount, string accountNumber, string routingNumber)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();

            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertTockenAmountInfo"; // Stored procedure name
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@propertyID", propertyId);
            objCommand.Parameters.AddWithValue("@AccountNumber", accountNumber);
            objCommand.Parameters.AddWithValue("@RoutingNumber", routingNumber);
            objCommand.Parameters.AddWithValue("@Amount", amount);
            objDB.DoUpdateUsingCmdObj(objCommand);


        }
    }
}
