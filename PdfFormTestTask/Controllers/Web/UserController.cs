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
        /// <summary>
        /// Initial Screen
        /// GET /
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            Session[Constants.USERNAME] = null;
            Session[Constants.PASSWORD] = null;

            return View();
        }

        /// <summary>
        /// Perform Log in 
        /// POST /
        /// </summary>
        /// <param name="_user">User's Model</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(PfsUser _user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PfsResponse<PfsUser> resp = RESTClient.GetUser(_user.Username, _user.Password);
                    //if user exists
                    if (resp.IsOk)
                    {
                        PfsUser user = resp.Data;
                        Session[Constants.USERNAME] = user.Username;
                        Session[Constants.PASSWORD] = user.Password;

                        return RedirectToAction("List", "Forms");
                    }
                    else
                    {
                        Danger(resp.Message, true);
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