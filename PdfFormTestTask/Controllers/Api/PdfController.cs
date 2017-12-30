using Aspose.Pdf;
using Aspose.Pdf.Forms;
using PdfFormTestTask.Client;
using PdfFormTestTask.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace PdfFormTestTask.Service.Controllers.Api
{
    public class PdfController : ApiController
    {
        /// <summary>
        /// Gets List of Fields of given PDF Form 
        /// GET api/Pdf/{username}/{password}/{id}
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="id">File Identifier</param>
        /// <returns>PfsResponse<List<PfsFormField>></returns>
        public PfsResponse<List<PfsFormField>> Get(string username, string password, string id)
        {
            try
            {
                PfsUser user = PfsRepository.Current.GetUser(username, password);
                if (null == user) return new PfsResponse<List<PfsFormField>>("Wrong Username or Password");

                PfsPdfFile pdfFile = user.GetPdfFileByLocalName(id);
                if (null == pdfFile) return new PfsResponse<List<PfsFormField>>("File doesn't exist");

                List<PfsFormField> data = new List<PfsFormField>();

                if (null != pdfFile)
                {
                    using (Document document = new Document(HttpContext.Current.Server.MapPath("~/App_Data") + "/" + pdfFile.LocalName))
                    {
                        for (int i = 1; i <= document.Form.Count; i++)
                        {
                            // some forms have Count of fields more than it really is. Sad but true.
                            try
                            {
                                data.Add(new PfsFormField(document.Form[i] as Field));
                            }
                            catch { }
                        }
                    }
                }
                return new PfsResponse<List<PfsFormField>>(data);
            }
            catch (Exception ex)
            {
                return new PfsResponse<List<PfsFormField>>(ex.Message);
            }
            
        }

        /// <summary>
        /// Save Fields Values of given PDF Form 
        /// POST api/Pdf/{username}/{password}/{id}
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="id">File Identifier</param>
        /// <param name="values">Post Data: List of PDF Fields</param>
        /// <returns>PfsResponse<object> (Message only)</returns>
        public PfsResponse<object> Post(string username, string password, string id, [FromBody] List<PfsFormField> values)
        {
            try
            {
                PfsUser user = PfsRepository.Current.GetUser(username, password);
                if (null == user) return new PfsResponse<object>("Wrong Username or Password");

                PfsPdfFile pdfFile = user.GetPdfFileByLocalName(id);
                if (null == pdfFile) return new PfsResponse<object>("File doesn't exist");

                string documentPath = HttpContext.Current.Server.MapPath("~/App_Data") + "/" + pdfFile.LocalName;

                using (Document document = new Document(documentPath))
                {
                    for (int i = 1; i <= document.Form.Count; i++)
                    {
                        Field field = null;
                        try
                        {
                            field = document.Form[i] as Field;
                        }
                        catch { }

                        if (null == field) continue;

                        foreach (PfsFormField pfsField in values)
                        {
                            if (field.FullName == pfsField.Name)
                            {

                                field.Value = pfsField.Value;
                                if (field is CheckboxField)
                                {
                                    (field as CheckboxField).Checked = pfsField.Checked;
                                }
                                break;
                            }
                        }
                    }

                    document.Save(documentPath);

                    
                }
                return new PfsResponse<object>("Success", true);
            }
            catch (Exception ex)
            {
                return new PfsResponse<object>(ex.Message);
            }
            
        }
    }
}
