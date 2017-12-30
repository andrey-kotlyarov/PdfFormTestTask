using PdfFormTestTask.Client;
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
        /// <summary>
        /// Get User's Model
        /// GET api/User/{username}/{password}
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">password</param>
        /// <returns>PfsResponse<PfsUser></returns>
        public PfsResponse<PfsUser> Get(string username, string password)
        {
            PfsUser ret = PfsRepository.Current.GetUser(username, password);
            if (null == ret)
            {
                return new PfsResponse<PfsUser>("Wrong Username or Pasword");
            }
            else
            {
                return new PfsResponse<PfsUser>(ret);
            }
        }
    }
}
