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
    }

}

