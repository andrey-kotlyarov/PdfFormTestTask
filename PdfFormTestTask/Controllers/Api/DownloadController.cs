﻿using PdfFormTestTask.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace PdfFormTestTask.Service.Controllers.Api
{
    public class DownloadController : ApiController
    {
        /// <summary>
        /// Gets Pdf Form File
        /// GET api/Download/{username}/{password}/{id}
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="id">File Identifier</param>
        /// <returns>HttpResponseMessage</returns>
        [HttpGet]
        public HttpResponseMessage DownloadFile(string username, string password, string id)
        {
            username = Base64Helper.Decode(username);
            password = Base64Helper.Decode(password);

            PfsPdfFile pdfFile = PfsRepository.Current.GetUser(username, password).GetPdfFileByLocalName(id);
            List<PfsFormField> ret = new List<PfsFormField>();

            string documentPath = HttpContext.Current.Server.MapPath("~/App_Data") + "/" + pdfFile.LocalName;

            FileStream fileStream = File.OpenRead(documentPath);
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(fileStream)
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = pdfFile.FileName
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
        }
    }
}
