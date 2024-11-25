using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class AgentInfo
    {
        // Private fields
        public string userID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street{ get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipCode { get; set; }

        public string phoneNumber { get; set; }
        public string email { get; set; }

        public AgentInfo()
        {

        }

        public AgentInfo(string userID, string firstName, string lastName, string street, string city, string state, int zipCode, string phoneNumber, string email)
        {
            this.userID = userID;
            this .firstName = firstName;    
            this.lastName = lastName;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }


        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();

    
        /*get agent informanation based on the propertyID
         retnun the data to the userclass*/
        public AgentInfo AgentContactInfo(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAgentInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            DataSet propertyDataSet = objDB.GetDataSetUsingCmdObj(objCommand);

            DataTable dtUserContact = propertyDataSet.Tables[0];
            if (dtUserContact.Rows.Count > 0)
            {
                DataRow userRow = dtUserContact.Rows[0];
                string userId = "";
                string firstName = userRow["FirstName"].ToString();
                string lastName = userRow["LastName"].ToString();
                string street = userRow["Street"].ToString();
                string city = userRow["City"].ToString();
                string state = userRow["State"].ToString();
                int zipCode = Convert.ToInt32(userRow["ZipCode"]);
                string phoneNumber = userRow["PhoneNumber"].ToString();
                string email = userRow["Email"].ToString();
                AgentInfo agentInfo = new AgentInfo(userId, firstName, lastName, street, city, state, zipCode, phoneNumber, email);
                return agentInfo;

            }

            else
            {
                throw new Exception("No user contact information found.");
            }
        }
    }
}
