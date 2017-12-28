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
    public class UserController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            Session[Constants.USERNAME] = null;
            Session[Constants.PASSWORD] = null;

            return View();
        }

        [HttpPost]
        public ActionResult Login(PfsUser _user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PfsUser user = RESTClient.GetUser(_user.Username, _user.Password);
                    if (null != user)
                    {
                        Session[Constants.USERNAME] = user.Username;
                        Session[Constants.PASSWORD] = user.Password;

                        return RedirectToAction("List", "Forms");
                    }
                    else
                    {
                        Danger("Wrong Username or Password!", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Danger(ex.Message);
            }
            return View();
        }
    }
}