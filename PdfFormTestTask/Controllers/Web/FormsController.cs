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
        [HttpGet]
        public ActionResult List()
        {
            PfsUser user = RESTClient.GetUser(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString());
            if (null == user)
            {
                return RedirectToAction("Login", "User");
            }
            ViewBag.Title = "Forms";
            return View(user);
        }
    }
}