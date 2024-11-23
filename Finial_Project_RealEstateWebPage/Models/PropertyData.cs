using ClassLibrary;
using FunctionClassLibrary.AssociateClass;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PropertyData
    {
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        /*calculate the square feet from the Selected rooms*/
        public string CalculateTotalSquareFeet(List<Rooms> rooms)
        {
            decimal totalSquareFeet = 0;
            foreach (var room in rooms)
            {
                totalSquareFeet += room.Width * room.Length;
            }
            string totalSquareFeetStr = totalSquareFeet.ToString();
            return totalSquareFeetStr;
        }

        public DataSet PropertyDataInfo(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        /*get the property data from the database tables based on the propertyid
         * and sent to the home class */
        public HomeInfo GetHomeData(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyInfo";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            DataSet propertyData = objDB.GetDataSetUsingCmdObj(objCommand);
            HomeInfo homeInfo = new HomeInfo();
            DataRow propertyRow = propertyData.Tables[0].Rows[0];
            homeInfo.BuildingNumber = propertyRow["BuildingNumber"].ToString();
            homeInfo.AgentID = propertyRow["AgentID"].ToString();
            homeInfo.Street = propertyRow["Street"].ToString();
            homeInfo.City = propertyRow["City"].ToString();
            homeInfo.State = propertyRow["State"].ToString();
            // converts the value stored in the propertyRow["ZipCode"] to an integer(int).
            homeInfo.ZipCode = Convert.ToInt32(propertyRow["ZipCode"]);
            homeInfo.PropertyType = propertyRow["PropertyType"].ToString();
            // converts the value stored in the propertyRow[] to an integer(int).
            homeInfo.YearBuilt = Convert.ToInt32(propertyRow["YearBuilt"]);
            homeInfo.BedRooms = Convert.ToInt32(propertyRow["BedRooms"]);
            homeInfo.BathRooms = Convert.ToInt32(propertyRow["Bathrooms"]);
            homeInfo.Heating = propertyRow["Heating"].ToString();
            homeInfo.Cooling = propertyRow["Cooling"].ToString();
            // converts the value stored in the propertyRow["AskingPrice"] to an integer(int).
            homeInfo.AskingPrice = Convert.ToDecimal(propertyRow["AskingPrice"]);
            homeInfo.HomeSize = propertyRow["HomeSize"].ToString();
            homeInfo.Description = propertyRow["Description"].ToString();
            homeInfo.Garage = propertyRow["Garage"].ToString();

            Console.WriteLine("Available in the result set.1");
           

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
           /* homes = new HomeInfo(buildingNumber, propertyID, agentID, street, city, state, zipCode,
                                  propertyType, yearBuilt, bedRooms, bathRooms, heating, cooling,
                                  askingPrice, homeSize, description, garage, utilities, amenities,
                                  rooms, images);*/
            return homeInfo;
        }
        /*methode to get the partial data from the user and show it to the homepage
         */
        public DataSet GetPropertyData()
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyData";
            objCommand.Parameters.Clear();
            return objDB.GetDataSetUsingCmdObj(objCommand);
        }
        /*methode to write property review
         * its take the review and data propery id and add it in the database*/
        public void AddPropertyReview(string propertyID, string review, DateTime reviewDate)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "InsertUserReview";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objCommand.Parameters.AddWithValue("@Review", review);
            objCommand.Parameters.AddWithValue("@DateTime", reviewDate);
            objDB.DoUpdateUsingCmdObj(objCommand);
        }
        /*methde to get the property review based on the property id
         and retun it in a string*/
        public string GetPropertyReview(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetUserReview"; // Stored procedure name
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objDB.DoUpdateUsingCmdObj(objCommand);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder reviews = new StringBuilder();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string review = row["PropertyReview"].ToString();
                    string date = Convert.ToDateTime(row["Date"]).ToString("MM-dd-yyyy");
                    reviews.AppendLine($"{date}: <br/> {review} <br/>");
                }
                return reviews.ToString();
            }
            else
            {
                return "No reviews found for this property.";
            }
        }
    }
}
