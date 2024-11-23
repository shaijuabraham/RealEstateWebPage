using System.Data.SqlClient;
using System.Data;
using Utilities;

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

        public Home()
        {

        }

        public Home(double askingPrice, int bedRooms, int bathRooms, string homeSize, 
            string street, string city, string state, int zipCode)
        {
            this.askingPrice = askingPrice;
            this.bedRooms = bedRooms;
            this.bathRooms = bathRooms;
            this.homeSize = homeSize;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
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
        public List<Home> GetPartalHomedata(string userId)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
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

    }
}
