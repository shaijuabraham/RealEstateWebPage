using System;
using Newtonsoft.Json;
using Finial_Project_RealEstateWebPage.Models;
using System.Net;



namespace WebApplicationApi.Models
{
    public class apiHandler
    {
        string webApiUrl = "https://localhost:44397/";

        public apiHandler()
        {
        }

        public Boolean AddProperty(HomeInfo homeInfo)
        {
            //Serialize
            String json = Newtonsoft.Json.JsonConvert.SerializeObject(homeInfo);
            //Create URL (to actually connect to the API)
            String url = webApiUrl + "api/home/AddProperty";
            WebRequest request = WebRequest.Create(url);

            //set up the request
            request.Method = "POST";
            request.ContentLength = json.Length;
            request.ContentType = "application/json";

            //write the request
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(json);
            writer.Flush();
            writer.Close();

            //get the response
            WebResponse response = request.GetResponse();

            //convert the response
            Stream theDataStream = response.GetResponseStream();

            //read the response
            StreamReader reader = new StreamReader(theDataStream);
            String data = reader.ReadToEnd();

            //deserialize the response
            Boolean result = JsonConvert.DeserializeObject<Boolean>(data);

            //return the response
            return result;
        }
    }
}
