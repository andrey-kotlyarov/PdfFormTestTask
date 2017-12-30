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
    /// <summary>
    /// Client for REST Service
    /// </summary>
    public static class RESTClient
    {
        private static string SERVICE_URL = "http://localhost:12333/api/";

        /// <summary>
        /// Gets PfsUser by Username and Password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public static PfsResponse<PfsUser> GetUser(string username, string password)
        {
            return JsonConvert.DeserializeObject<PfsResponse<PfsUser>>(Get("User/" + username + "/" + password)) as PfsResponse<PfsUser>;
        }

        /// <summary>
        /// Gets List of fields of form 
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="fileName">File identifier</param>
        /// <returns></returns>
        public static PfsResponse<List<PfsFormField>> GetFormList(string username, string password, string fileName)
        {
            string response = Get("Pdf/" + username + "/" + password + "/" + fileName);
            return JsonConvert.DeserializeObject< PfsResponse<List<PfsFormField>>>(response) as PfsResponse<List<PfsFormField>>;
        }

        /// <summary>
        /// Sets values of forms
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="fileName">File identifier</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public static PfsResponse<object> PostValues(string username, string password, string fileName, List<PfsFormField> fields)
        {
            string response = Post("Pdf/" + username + "/" + password + "/" + fileName, JsonConvert.SerializeObject(fields));

            return JsonConvert.DeserializeObject<PfsResponse<object>>(response) as PfsResponse<object>;
        }

        /// <summary>
        /// Makes a Post Request
        /// </summary>
        /// <param name="query">Url Query</param>
        /// <param name="data">Post Data</param>
        /// <returns></returns>
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

        /// <summary>
        /// Makes a Get Request
        /// </summary>
        /// <param name="query">Url Query</param>
        /// <returns></returns>
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
