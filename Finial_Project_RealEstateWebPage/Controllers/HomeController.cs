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
        [HttpGet]
        public IActionResult HomePageRun()
        {
            Home home = new Home();

            DBConnect objDB = new DBConnect();
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandText = "GetPropertyData";
            command.Parameters.Clear();
            DataSet ds = objDB.GetDataSetUsingCmdObj(command);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow record = ds.Tables[0].Rows[0];
                home.AskingPrice = float.Parse(record["AskingPrice"].ToString());
                home.BedRooms = int.Parse(record["BedRooms"].ToString());
                home.BathRooms = int.Parse(record["BathRooms"].ToString());
                home.HomeSize = record["HomeSize"].ToString();
                home.Street = record["Street"].ToString();
                home.City = record["City"].ToString();
                home.Zip = int.Parse(record["Zip"].ToString());
            }

            return View(home);
        }

    }
}
