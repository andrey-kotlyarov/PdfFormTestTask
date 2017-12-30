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
            
            PfsResponse<PfsUser> userResponse = RESTClient.GetUser(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString());

            if (!userResponse.IsOk)
            {
                return RedirectToAction("Login", "User");
            }

            PfsUser user = userResponse.Data;

            PfsPdfFile file = user.GetPdfFileByLocalName(id);
            if (null == file)
            {
                return RedirectToAction("List", "Forms");
            }

            ViewBag.Title = "PDF Form Fields: " + file.FileName;

            PfsResponse<List<PfsFormField>> fieldsResponse = RESTClient.GetFormList(user.Username, user.Password, file.LocalName);

            if (!fieldsResponse.IsOk)
            {
                Danger(fieldsResponse.Message);
                return View(new List<PfsFormField>());
            }
            return View(fieldsResponse.Data);
        }

        /// <summary>
        /// Save fields values and redirect to list of forms
        /// POST Edit
        /// </summary>
        /// <param name="fields">List<PfsFormField></param>
        /// <param name="id">File identifier</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult Index(List<PfsFormField> fields, string id)
        {
            PfsResponse<object> ret = RESTClient.PostValues(Session[Constants.USERNAME].ToString(), Session[Constants.PASSWORD].ToString(), id, fields);
            if (ret.IsOk)
            {
                return RedirectToAction("List", "Forms");
            }
            else
            {
                return View(new List<PfsFormField>());
            }
        }
    }
}