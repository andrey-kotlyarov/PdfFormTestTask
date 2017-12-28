using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Model
{
    public class PfsUser
    {
        public IList<PfsPdfFile> PdfFiles = new List<PfsPdfFile>();

        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required field")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required field")]
        public string Password { get; set; }

        public PfsUser()
        {
        }

        public void AddPdfForm(string fileName, string localFileName)
        {
            PdfFiles.Add(new PfsPdfFile() { LocalName = localFileName, FileName = fileName });
        }

        public PfsPdfFile GetPdfFileByLocalName(string localName)
        {
            return PdfFiles
                .Where(p => p.LocalName == localName)
                .FirstOrDefault();
        }
    }
}
