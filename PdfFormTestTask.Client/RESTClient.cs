using Newtonsoft.Json;
using PdfFormTestTask.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Client
{
    public class RESTClient
    {
        private static string SERVICE_URL = "http://localhost:12333/api/";

        /// <summary>
        /// Gets PfsUser by Username and Password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public static PfsUser GetUser(string username, string password)
        {
            return JsonConvert.DeserializeObject<PfsUser>(Get("User/" + username + "/" + password)) as PfsUser;
        }

        public static List<PfsFormField> GetFormList(string username, string password, string fileName)
        {
            string response = Get("Pdf/" + username + "/" + password + "/" + fileName);
            return JsonConvert.DeserializeObject<List<PfsFormField>>(response) as List<PfsFormField>;
        }

        public static List<PfsFormField> PostValues(string username, string password, string fileName, List<PfsFormField> fields)
        {
            string response = Post("Pdf/" + username + "/" + password + "/" + fileName, JsonConvert.SerializeObject(fields));

            return JsonConvert.DeserializeObject<List<PfsFormField>>(response) as List<PfsFormField>;
        }

        private static string Post(string query, string data)
        {
            WebRequest request = WebRequest.Create(SERVICE_URL + query);
            request.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            // Set the ContentType property of the WebRequest.  
            request.ContentType = "application/json";
            // Set the ContentLength property of the WebRequest.  
            request.ContentLength = byteArray.Length;
            // Get the request stream.  
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.  
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.  
            dataStream.Close();
            // Get the response.  
            WebResponse response = request.GetResponse();
            // Display the status.  
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.  
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.  
            string responseFromServer = reader.ReadToEnd();
            // Display the content.  
            Console.WriteLine(responseFromServer);
            // Clean up the streams.  
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private static string Get(string query)
        {
            // Create a request for the URL. 
            WebRequest request = WebRequest.Create(
                SERVICE_URL + query
              );

            request.Method = "GET";

            // If required by the server, set the credentials.
            request.Credentials = CredentialCache.DefaultCredentials;



            // Get the response.
            WebResponse response = request.GetResponse();

            // Get the stream containing content returned by the server.
            Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams and the response.
            reader.Close();
            response.Close();

            return responseFromServer;
        }



    }
}
