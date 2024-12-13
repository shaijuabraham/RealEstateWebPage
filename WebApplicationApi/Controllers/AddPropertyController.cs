using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationApi.Controllers
{
    public class AddPropertyController : ControllerBase
    {


        [Route("api/home/AddProperty")]
        [HttpPost]
        public Boolean AddProperty([FromBody] HomeInfo homeInfo)
        {
            // Add the property to the database
                // Call your database object
            PropertyDataInfo dbCall = new PropertyDataInfo();
            
            if (dbCall.AddProperty(homeInfo) > 0)
            {
                return true;
            }

            return false;

        }
    }
}
