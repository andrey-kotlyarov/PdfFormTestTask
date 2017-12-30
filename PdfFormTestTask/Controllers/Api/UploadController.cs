using PdfFormTestTask.Client;
using PdfFormTestTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace PdfFormTestTask.Service.Controllers.Api
{
    public class UploadController : ApiController
    {
        /// <summary>
        /// Uploads Pdf Form to the server 
        /// POST api/Upload/{username}/{password}
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>PfsResponse<object></returns>
        public async Task<PfsResponse<object>> PostFormData(string username, string password)
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);
                
                // Adding new PDF Form to Model
                foreach (MultipartFileData file in provider.FileData)
                {
                    PfsRepository.Current.GetUser(username, password).AddPdfForm(file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                        file.LocalFileName.Split('\\').Last());
                }
                //return Request.CreateResponse(HttpStatusCode.OK);
                return new PfsResponse<object>("Success", true);
            }
            catch (System.Exception ex)
            {
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
                return new PfsResponse<object>(ex.Message);
            }
        }
    }
}
