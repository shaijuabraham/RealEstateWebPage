using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class AccountRegistration
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactStreet { get; set; }
        public string ContactCity { get; set; }
        public string ContactState { get; set; }
        public int ContactZipCode { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }

        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyState { get; set; }
        public int CompanyZipCode { get; set; }
        public string CompanyPhoneNumber { get; set; }
        public string CompanyEmail { get; set; }

        public string LicenseNumber { get; set; }
        public string BrokerType { get; set; }
        public string Password { get; set; }


        public AccountRegistration()
        {}

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();


        /*Takes the userinformanation from the siginup page and add int in the 
         nessery database table
        each methode takes the vales fro each database table and store it there*/
        public void AccountPersonalInfoSignUp(string userID, string firstName, string lastName, string street,
            string city, string state, int zipCode, string phoneNumber, string email)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertUserPersonalInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@UserID", userID);
            objCommand.Parameters.AddWithValue("@FirstName", firstName);
            objCommand.Parameters.AddWithValue("@LastName", lastName);
            objCommand.Parameters.AddWithValue("@Street", street);
            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);
            objCommand.Parameters.AddWithValue("@ZipCode", zipCode);
            objCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            objCommand.Parameters.AddWithValue("@Email", email);
            objDB.DoUpdateUsingCmdObj(objCommand);

        }

        public void AccountContactInfoSignUp(string userID, string firstName, string lastName, string street,
            string city, string state, int zipCode, string phoneNumber, string email)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertUserContactInfo"; // Stored procedure name
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@UserID", userID);
            objCommand.Parameters.AddWithValue("@FirstName", firstName);
            objCommand.Parameters.AddWithValue("@LastName", lastName);
            objCommand.Parameters.AddWithValue("@Street", street);
            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);
            objCommand.Parameters.AddWithValue("@ZipCode", zipCode);
            objCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            objCommand.Parameters.AddWithValue("@Email", email);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

        public void UserAccountSignUp(string userID, string companyName, string street, string city,
                                        string state, int zipCode, string phoneNumber, string email,
                                        string licenseNumber, string brokerType, string password)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertAccountInfo"; // Stored procedure name
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@UserID", userID);
            objCommand.Parameters.AddWithValue("@CompanyName", companyName);
            objCommand.Parameters.AddWithValue("@Street", street);
            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);
            objCommand.Parameters.AddWithValue("@ZipCode", zipCode);
            objCommand.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
            objCommand.Parameters.AddWithValue("@Email", email);
            objCommand.Parameters.AddWithValue("@LicenseNumber", licenseNumber);
            objCommand.Parameters.AddWithValue("@BrokerType", brokerType);
            objCommand.Parameters.AddWithValue("@Password", password);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }

    }
}
