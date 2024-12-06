using FunctionClassLibrary.AssociateClass;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models.associateclass
{
    public class AgentCompanyInfo
    {
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public AgentCompanyInfo() { }
        public AgentCompanyInfo(string companyName, string street, string city, string state, int zipCode, string phoneNumber, string email)
        {
            CompanyName = companyName;
            Street = street;
            City = city; 
            State = state;
            ZipCode = zipCode;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*get agent company informanation based on the propertyID
        and retun teh data to the company class*/


        public AgentCompanyInfo RelatorCompanyInfo(string propertyID)
        {
            objCommand.Parameters.Clear();
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAgentCompanyInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            DataSet companyDataSet = objDB.GetDataSetUsingCmdObj(objCommand);
            DataTable dtCompanyInfo = companyDataSet.Tables[0];
            if (dtCompanyInfo.Rows.Count > 0)
            {
                DataRow companyRow = dtCompanyInfo.Rows[0];
                string firstName = companyRow["CompanyName"].ToString();
                string street = companyRow["Street"].ToString();
                string city = companyRow["City"].ToString();
                string state = companyRow["State"].ToString();
                int zipCode = Convert.ToInt32(companyRow["ZipCode"]);
                string phoneNumber = companyRow["PhoneNumber"].ToString();
                string email = companyRow["Email"].ToString();
                AgentCompanyInfo companyInfo = new AgentCompanyInfo(firstName, street, city, state, zipCode, phoneNumber, email);
                return companyInfo;
            }
            else
            {
                throw new Exception("No company information found.");
            }
        }
    }
}
 