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

    }
}
