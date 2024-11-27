using Finial_Project_RealEstateWebPage.Controllers;
using Finial_Project_RealEstateWebPage.Models.associateclass;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Utilities;

namespace Finial_Project_RealEstateWebPage.Models
{
    
    public class PropertyDetails
    {
        public HomeInfo HomeInfo { get; set; }
        public AgentInfo AgentInfo { get; set; }

        public AgentCompanyInfo AgentCompanyInfo { get; set; }

        public Home Home { get; set; }
       

    }
}
