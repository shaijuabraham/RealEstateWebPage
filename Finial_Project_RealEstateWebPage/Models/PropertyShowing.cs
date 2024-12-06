using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Utilities;
using Microsoft.Extensions.Logging;

namespace Finial_Project_RealEstateWebPage.Models
{
    public class PropertyShowing
    {

        public int HomeShowingID {  get; set; }
        public String FullName { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Date {  get; set; }

        public String Time { get; set; }
        public String PropertyID { get; set; }

        public PropertyShowing() { }
        public PropertyShowing(string fullname, string email, string phonenumber, 
            string date, string time, string propertyId)
        {
            this.FullName = fullname;
            this.Email = email;
            this.PhoneNumber = phonenumber;
            this.Date = date;
            this.Time = time;
            this.PropertyID = propertyId;
        }

        public List<PropertyShowing> PropertyDataInfo(string agentID)
        {
            DBConnect objDB = new DBConnect();
            SqlCommand objCommand = new SqlCommand();
            Console.WriteLine("Available in the result set.1");
            List<PropertyShowing> homes = new List<PropertyShowing>();
            try
            {

                objCommand.CommandType = CommandType.StoredProcedure;
                objCommand.CommandText = "GetPropertyShowingRequests";
                objCommand.Parameters.Clear();
                objCommand.Parameters.AddWithValue("@AgentID", agentID);
                DataSet ds = objDB.GetDataSetUsingCmdObj(objCommand);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow propertyRow in ds.Tables[0].Rows)
                    {
                        PropertyShowing home = new PropertyShowing();

                        if (propertyRow.Table.Rows.Count > 0)
                        {
                            home.HomeShowingID = int.Parse(propertyRow["Id"].ToString());
                            home.FullName = propertyRow["FullName"].ToString();
                            home.Email = propertyRow["Email"].ToString();
                            home.PhoneNumber = propertyRow["PhoneNumber"].ToString();
                            home.Date = propertyRow["Date"].ToString();
                            home.Time = propertyRow["Time"].ToString();
                            home.PropertyID = propertyRow["PropertyId"].ToString();
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



