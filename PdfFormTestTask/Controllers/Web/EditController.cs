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
    public class EditController : BaseController
    {
        // GET: Edit

        /// <summary>
        /// Show fields of form
        /// GET Edit
        /// </summary>
        /// <param name="id">File Identifier</param>
        /// <returns>List of form's fields</returns>
        [HttpGet]
        public ActionResult Index(string id)
        {
            
            PfsUser user = RESTClient.GetUser(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString());

            if (null == user)
            {
                return RedirectToAction("Login", "User");
            }

            PfsPdfFile file = user.GetPdfFileByLocalName(id);
            if (null == file)
            {
                return RedirectToAction("List", "Forms");
            }

            ViewBag.Title = "PDF Form Fields: " + file.FileName;

            List<PfsFormField> fields = RESTClient.GetFormList(user.Username, user.Password, file.LocalName);

            return View(fields);
        }

        /// <summary>
        /// Save fields values and redirect to list of forms
        /// POST Edit
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(List<PfsFormField> fields, string id)
        {
            List<PfsFormField> ret = RESTClient.PostValues(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString(), id, fields);

            return RedirectToAction("List", "Forms");
        }
    }
}