using System.Data.SqlClient;
using System.Data;
using Utilities;
using System.Data.Common;
using System.Text;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class Home
    {
        private double askingPrice;
        public string BuildingNumber {  get; set; }
        public string PropertyID { get; set; }
        private int bedRooms;
        private int bathRooms;
        private string homeSize;
        private string street;
        private string city;
        private string state;
        private int zipCode;

        public string DaysAvliabeOnMarket { get; set; }
        public string PropertyStatus { get; set; }

        public string PriceHistory { get; set; }
        public string UserReview { get; set; }

        public Home()
        {

        }
        public double AskingPrice
        {
            get { return askingPrice; }
            set { askingPrice = value; }
        }

        public int BedRooms
        {
            get { return bedRooms; }
            set { bedRooms = value; }
        }

        public int BathRooms
        {
            get { return bathRooms; }
            set { bathRooms = value; }
        }

        public string HomeSize
        {
            get { return homeSize; }
            set { homeSize = value; }
        }

        public string Street
        {
            get { return street; }
            set { street = value; }
        }

        public string City
        {
            get { return city; }
            set { city = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public int ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        public List<Home> GetPartalHomedata()
        {
            List<Home> homes = new List<Home>();

            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetPropertyData"
                };
                command.Parameters.Clear();
                DataSet ds = objDB.GetDataSetUsingCmdObj(command);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow record in ds.Tables[0].Rows)
                    {
                        Home home = new Home();

                        if (record.Table.Rows.Count > 0)
                        {
                            home.AskingPrice = double.Parse(record["AskingPrice"].ToString());
                            home.BuildingNumber = record["BuildingNumber"].ToString();
                            home.BedRooms = int.Parse(record["BedRooms"].ToString());
                            home.BathRooms = int.Parse(record["Bathrooms"].ToString());
                            home.HomeSize = record["HomeSize"].ToString();
                            home.Street = record["Street"].ToString();
                            home.City = record["City"].ToString();
                            home.State = record["State"].ToString();
                            home.ZipCode = int.Parse(record["ZipCode"].ToString());
                            home.BuildingNumber = record["BuildingNumber"].ToString();
                            home.PropertyID = record["PropertyID"].ToString();
                        }

                        homes.Add(home);
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




        /*get the partal data from the database when relator userid 
         match the datatable*/
        DBConnect objDB = new DBConnect();
        SqlCommand objCommand = new SqlCommand();
        public List<Home> GetPartalHomedata(string userId)
        {
            Console.WriteLine("Available in the result set.1");
            List<Home> homes = new List<Home>();
            try
            {

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetRealtorPropertyList";
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@UserID", userId);
                DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow record in ds.Tables[0].Rows)
                    {
                        Home home = new Home();

                        if (record.Table.Rows.Count > 0)
                        {
                            home.AskingPrice = double.Parse(record["AskingPrice"].ToString());
                            home.BuildingNumber = record["BuildingNumber"].ToString();
                            home.BedRooms = int.Parse(record["BedRooms"].ToString());
                            home.BathRooms = int.Parse(record["Bathrooms"].ToString());
                            home.HomeSize = record["HomeSize"].ToString();
                            home.Street = record["Street"].ToString();
                            home.City = record["City"].ToString();
                            home.State = record["State"].ToString();
                            home.ZipCode = int.Parse(record["ZipCode"].ToString());
                            home.PropertyID = record["PropertyID"].ToString();
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


        /*methode to get the property status from database and put it in a string builder*/
        public string GetPropertyStatus(string propertyID)
        {
            
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyStatus";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyID", propertyID);
            objDB.DoUpdateUsingCmdObj(objCommand);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                string status = row["PropertyStatus"].ToString();
                string date = Convert.ToDateTime(row["ListingDate"]).ToString("MM-dd-yyyy");
                return $"Current Status: {status} (Last Updated : {date})";
            }
            else
            {
                return "No status record found for this property.";
            }
        }


        /*this methode show the property pricehistory on Viewhome page 
        when user select teh perioerty by retun it ina string */
        public string ShowPriceHistory(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetPropertyPriceHistory";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder priceHistory = new StringBuilder();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string price = Convert.ToDecimal(row["Price"]).ToString("C");
                    string date = Convert.ToDateTime(row["Date"]).ToString("MM-dd-yyyy");
                    priceHistory.AppendLine($"{date}:  {price}");
                }
                return priceHistory.ToString();
            }
            else
            {
                return "No price change for this property.";
            }
        }

        /*this methdoe will show the number of days avlaibe in the market */
        public string GetpropertyDate(string propertyID)
        {
            objCommand.CommandType = CommandType.StoredProcedure;
            objCommand.CommandText = "GetAddedDate";
            objCommand.Parameters.Clear();
            objCommand.Parameters.AddWithValue("@PropertyId", propertyID);
            DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder addeddate = new StringBuilder();
                foreach (DataRow row in ds.Tables[0].Rows)
                {/*add the date to the date vale from the selected row*/
                    DateTime date = Convert.ToDateTime(row["dateAdded"]);
                    addeddate.AppendLine($"Days On Market: {GetpropertyaddedDate(date)} days");
                }
                return addeddate.ToString();
            }
            else
            {
                return "Days ON Market : Not Avliable";
            }
        }
        //methode to claculate the get the property date.
        public int GetpropertyaddedDate(DateTime date)
        {
            DateTime dateTime = DateTime.Now;
            /*user time span to way to store the difference between days*/
            TimeSpan difference = dateTime - date;
            int days = difference.Days;
            return days;
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
                    reviews.AppendLine($"{date}: {review}");
                }
                return reviews.ToString();
            }
            else
            {
                return "No reviews found for this property.";
            }
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
    }
}
