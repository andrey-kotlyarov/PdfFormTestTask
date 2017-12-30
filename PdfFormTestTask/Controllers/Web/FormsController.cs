using PdfFormTestTask.Client;
using PdfFormTestTask.Model;
using PdfFormTestTask.Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PdfFormTestTask.Service.Controllers.Web
{
    public class FormsController : BaseController
    {
        /// <summary>
        /// List of Uploaded Forms
        /// GET Forms/List
        /// </summary>
        /// <returns>View with list of Forms</returns>
        [HttpGet]
        public ActionResult List()
        {
            PfsResponse<PfsUser> resp = RESTClient.GetUser(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString());

            if (!resp.IsOk)
            {
                // if user doesn't exist - redirect to login page
                return RedirectToAction("Login", "User");
            }
            ViewBag.Title = "Forms";
            return View(resp.Data);
        }
    }
}