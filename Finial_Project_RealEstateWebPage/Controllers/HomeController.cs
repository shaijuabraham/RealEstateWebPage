using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Mvc;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Controllers
{
    public class HomeController : Controller
    {
        // Checkig if the comit works



        [HttpGet]
        public IActionResult HomePageRun()
        {
            List<Home> homes = new List<Home>();

            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetPropertyData";

                DataSet ds = objDB.GetDataSetUsingCmdObj(command);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int totalRows = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < totalRows; i++)
                    {
                        DataRow record = ds.Tables[0].Rows[i];
                        Home home = new Home();

                        if (!record.IsNull("AskingPrice"))
                            home.AskingPrice = double.Parse(record["AskingPrice"].ToString());

                        if (!record.IsNull("BedRooms"))
                            home.BedRooms = int.Parse(record["BedRooms"].ToString());

                        if (!record.IsNull("Bathrooms"))
                            home.BathRooms = int.Parse(record["Bathrooms"].ToString());

                        if (!record.IsNull("HomeSize"))
                            home.HomeSize = record["HomeSize"].ToString();

                        if (!record.IsNull("Street"))
                            home.Street = record["Street"].ToString();

                        if (!record.IsNull("City"))
                            home.City = record["City"].ToString();

                        if (!record.IsNull("State"))
                            home.State = record["State"].ToString();

                        if (!record.IsNull("ZipCode"))
                            home.ZipCode = int.Parse(record["ZipCode"].ToString());

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

            ViewBag.HomesList = homes; 
            return View("~/Views/Home/HomePage.cshtml");

        }

        [HttpPost]
        public IActionResult SearchHomes(string search, string propertyType)
        {
            List<Home> homes = new List<Home>();

            try
            {
                DBConnect objDB = new DBConnect();
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "SearchPropertyList";

                // Add parameters based on the search input
                if (!string.IsNullOrEmpty(search))
                {
                    int zipCode;
                    if (int.TryParse(search, out zipCode))
                    {
                        command.Parameters.Add(new SqlParameter("@ZipCode", zipCode));
                    }
                    else
                    {
                        // Search by city or state if not a zip code
                        command.Parameters.Add(new SqlParameter("@City", search));
                        command.Parameters.Add(new SqlParameter("@State", search));
                    }
                }

                if (!string.IsNullOrEmpty(propertyType))
                {
                    command.Parameters.Add(new SqlParameter("@PropertyType", propertyType));
                }

                DataSet ds = objDB.GetDataSetUsingCmdObj(command);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow record in ds.Tables[0].Rows)
                    {
                        Home home = new Home();

                        // Populate home properties from DataRow
                        if (!record.IsNull("AskingPrice"))
                            home.AskingPrice = Convert.ToDouble(record["AskingPrice"]);

                        if (!record.IsNull("BedRooms"))
                            home.BedRooms = Convert.ToInt32(record["BedRooms"]);

                        if (!record.IsNull("Bathrooms"))
                            home.BathRooms = Convert.ToInt32(record["Bathrooms"]);

                        if (!record.IsNull("HomeSize"))
                            home.HomeSize = record["HomeSize"].ToString();

                        if (!record.IsNull("Street"))
                            home.Street = record["Street"].ToString();

                        if (!record.IsNull("City"))
                            home.City = record["City"].ToString();

                        if (!record.IsNull("State"))
                            home.State = record["State"].ToString();

                        if (!record.IsNull("ZipCode"))
                            home.ZipCode = Convert.ToInt32(record["ZipCode"]);

                        homes.Add(home);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during search: " + ex.Message);
            }

            // Pass the list of homes to the view
            ViewBag.HomesList = homes;
            return View("~/Views/Search/SearchResults.cshtml");
        }



    }

}

