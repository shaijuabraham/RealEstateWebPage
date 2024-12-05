using ClassLibrary;
using FunctionClassLibrary.AssociateClass;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PropertyDataInfo
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        public HomeInfo GetHomeData(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            DataSet propertyData = objDB.GetDataSetUsingCmdObj(objCommand);

            DataRow propertyRow = propertyData.Tables[0].Rows[0];
            string buildingNumber = propertyRow["BuildingNumber"].ToString();
            string agentID = propertyRow["AgentID"].ToString();
            string street = propertyRow["Street"].ToString();
            string city = propertyRow["City"].ToString();
            string state = propertyRow["State"].ToString();
            // converts the value stored in the propertyRow["ZipCode"] to an integer(int).
            int zipCode = Convert.ToInt32(propertyRow["ZipCode"]);
            string propertyType = propertyRow["PropertyType"].ToString();
            // converts the value stored in the propertyRow[] to an integer(int).
            int yearBuilt = Convert.ToInt32(propertyRow["YearBuilt"]);
            int bedRooms = Convert.ToInt32(propertyRow["BedRooms"]);
            int bathRooms = Convert.ToInt32(propertyRow["Bathrooms"]);
            string heating = propertyRow["Heating"].ToString();
            string cooling = propertyRow["Cooling"].ToString();
            // converts the value stored in the propertyRow["AskingPrice"] to an integer(int).
            decimal askingPrice = Convert.ToDecimal(propertyRow["AskingPrice"]);
            string homeSize = propertyRow["HomeSize"].ToString();
            string description = propertyRow["Description"].ToString();
            string garage = propertyRow["Garage"].ToString();
            List<string> utilities = new List<string>();
            if (propertyData.Tables.Count > 2)
            {
                foreach (DataRow utilityRow in propertyData.Tables[2].Rows)
                {
                    utilities.Add(utilityRow["Utility"].ToString());
                }
            }
            List<string> amenities = new List<string>();
            if (propertyData.Tables.Count > 1)
            {
                foreach (DataRow amenityRow in propertyData.Tables[1].Rows)
                {
                    amenities.Add(amenityRow["Amenities"].ToString());
                }
            }
            List<Rooms> rooms = new List<Rooms>();
            if (propertyData.Tables.Count > 1)
            {
                foreach (DataRow roomRow in propertyData.Tables[4].Rows)
                {

                    string roomsName = roomRow["RoomName"].ToString();
                    int Width = Convert.ToInt32(roomRow["Width"]);
                    int Length = Convert.ToInt32(roomRow["Length"]);
                    int id = Convert.ToInt32(roomRow["Id"]);

                    Rooms rooms1 = new Rooms(id, Length, Width, roomsName);
                    rooms.Add(rooms1);
                }
            }
            List<PropertyImage> images = new List<PropertyImage>();
            HomeInfo home = new HomeInfo(buildingNumber, propertyID, agentID, street, city, state, zipCode,
                                  propertyType, yearBuilt, bedRooms, bathRooms, heating, cooling,
                                  askingPrice, homeSize, description, garage, utilities, amenities,
                                  rooms, images);
            return home;
        }

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
