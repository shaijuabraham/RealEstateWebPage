using Finial_Project_RealEstateWebPage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddPropertyController : ControllerBase
    {

        [HttpPost]
        public void AddProerty([FromBody] HomeInfo homeInfo)
        {
          
        }
    }
}
