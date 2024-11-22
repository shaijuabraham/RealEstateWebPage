using ClassLibrary;
using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PropertyInfo
    {

        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*the Methode toadd the property infro from the addhomepage 
         its add the data to the property tabel*/
        public void AddProperty(HomeInfo home)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertPropertyInfo"; // Stored procedure name
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", home.PropertyID);
            objCommand.Parameters.AddWithValue("@AgentID", home.AgentID);
            objCommand.Parameters.AddWithValue("@BuildingNumber", home.BuildingNumber);
            objCommand.Parameters.AddWithValue("@Street", home.Street);
            objCommand.Parameters.AddWithValue("@City", home.City);
            objCommand.Parameters.AddWithValue("@State", home.State);
            objCommand.Parameters.AddWithValue("@ZipCode", home.ZipCode);
            objCommand.Parameters.AddWithValue("@PropertyType", home.PropertyType);
            objCommand.Parameters.AddWithValue("@YearBuilt", home.YearBuilt);
            objCommand.Parameters.AddWithValue("@BedRooms", home.BedRooms);
            objCommand.Parameters.AddWithValue("@BathRooms", home.BathRooms);
            objCommand.Parameters.AddWithValue("@Heating", home.Heating);
            objCommand.Parameters.AddWithValue("@Cooling", home.Cooling);
            objCommand.Parameters.AddWithValue("@AskingPrice", home.AskingPrice);
            objCommand.Parameters.AddWithValue("@HomeSize", home.HomeSize);
            objCommand.Parameters.AddWithValue("@Description", home.Description);
            objCommand.Parameters.AddWithValue("@Garage", home.Garage);
            objDB.DoUpdateUsingCmdObj(objCommand);

        }
        /*Methode to craeate a property for each 
         * proerty added in the property page*/
        public string GeneratePropertyID(string input)
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 10000);
            string result = input + randomNumber.ToString();
            return result;
        }

        /*Methode toadd the Utilities,Amenities and roominformanation
         to the datatable and when user add the info its willtl sent to the 
        Home class and from home class the data will sent to the database */
        public void AddUtilities(HomeInfo home)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyUtilityInfo"; // Stored procedure name
            foreach (string utility in home.HomeUtility.SelectedUtility)
            {
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@PropertyID", home.PropertyID);
                objCommand.Parameters.AddWithValue("@Utility", utility);
                objDB.DoUpdateUsingCmdObj(objCommand);
            }
        }
        public void AddAmenities(HomeInfo home)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyAmenitiesInfo"; // Stored procedure name
            foreach (string amenities in home.HomeAmenities.SelectedAmenities)
            {
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@PropertyID", home.PropertyID);
                objCommand.Parameters.AddWithValue("@Amenities", amenities);
                objDB.DoUpdateUsingCmdObj(objCommand);
            }
        }

        public void AddPropertyRoomsInfo(HomeInfo home)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyRoomsInfo"; // Stored procedure name
            foreach (Rooms room in home.HomeRooms)
            {
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@PropertyID", home.PropertyID);
                objCommand.Parameters.AddWithValue("@RoomName", room.RoomName);
                objCommand.Parameters.AddWithValue("@Width", room.Width);
                objCommand.Parameters.AddWithValue("@Length", room.Length);
                objDB.DoUpdateUsingCmdObj(objCommand);
            }
        }
    }
}
