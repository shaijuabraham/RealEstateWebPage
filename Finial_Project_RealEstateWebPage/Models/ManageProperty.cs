using System.Data.SqlClient;
using System.Data;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class ManageProperty
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*this methode will update the property infor in the property table */
        public void UpdatedatePropertyInfo
            (string propertyID,
            string buildingNumber, string street, string city,
            string state, int zipCode, string propertyType, int yearBuilt,
            int bedRooms, int bathRooms, string heating,
            string cooling, decimal askingPrice,
            int homeSize, string description, string garage)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdatePropertyDetails";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@BuildingNumber", buildingNumber);
            objCommand.Parameters.AddWithValue("@Street", street);
            objCommand.Parameters.AddWithValue("@City", city);
            objCommand.Parameters.AddWithValue("@State", state);
            objCommand.Parameters.AddWithValue("@ZipCode", zipCode);
            objCommand.Parameters.AddWithValue("@PropertyType", propertyType);
            objCommand.Parameters.AddWithValue("@YearBuilt", yearBuilt);
            objCommand.Parameters.AddWithValue("@BedRooms", bedRooms);
            objCommand.Parameters.AddWithValue("@BathRooms", bathRooms);
            objCommand.Parameters.AddWithValue("@Heating", heating);
            objCommand.Parameters.AddWithValue("@Cooling", cooling);
            objCommand.Parameters.AddWithValue("@AskingPrice", askingPrice);
            objCommand.Parameters.AddWithValue("@HomeSize", homeSize);
            objCommand.Parameters.AddWithValue("@Description", description);
            objCommand.Parameters.AddWithValue("@Garage", garage);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*this methode will delete the each room based on the id*/
        public void DeleteRoominfo(int id)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "DeleteRoom";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@Id", id);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*methode to add the new rooom informanataion*/
        public void UpdatePropertyRoomsInfo(string propertyID, string roomName,
                                             int width, int length)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyRoomsInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@RoomName", roomName);
            objCommand.Parameters.AddWithValue("@Width", width);
            objCommand.Parameters.AddWithValue("@Length", length);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*methode to delete the previous Amenities and uselites based on the propertyid*/
        public void UpdatePropertyListInfo(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "UpdatePropertyList";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*methodes to add the anaities and utlities from  user info*/
        public void AddUtilities(string propertyID, List<string> utilities)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyUtilityInfo";
            foreach (var utilityList in utilities)
            {
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
                objCommand.Parameters.AddWithValue("@Utility", utilityList);
                objDB.DoUpdateUsingCmdObj(objCommand);
            }
        }
        public void AddAmenities(string propertyID, List<string> amenities)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "PropertyAmenitiesInfo";
            foreach (var amenitiesList in amenities)
            {
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
                objCommand.Parameters.AddWithValue("@Amenities", amenitiesList);
                objDB.DoUpdateUsingCmdObj(objCommand);

            }
        }

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
