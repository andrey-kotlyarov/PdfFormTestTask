using PdfFormTestTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PdfFormTestTask.Service.Controllers.Api
{
    public class UserController : ApiController
    {
        public PfsUser Get(string username, string password)
        {
            return PfsRepository.Current.GetUser(username, password);
        }
    }
}
