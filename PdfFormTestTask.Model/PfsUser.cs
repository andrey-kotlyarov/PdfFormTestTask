using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Model
{
    /// <summary>
    /// User's Model
    /// </summary>
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

        /// <summary>
        /// Adds PDF form doc to teh user 
        /// </summary>
        /// <param name="fileName">Original File Name</param>
        /// <param name="localFileName">File Identifier</param>
        public void AddPdfForm(string fileName, string localFileName)
        {
            PdfFiles.Add(new PfsPdfFile() { LocalName = localFileName, FileName = fileName });
        }

        /// <summary>
        /// Gets User's PDF Form by File Identifier
        /// </summary>
        /// <param name="localName">File Identifier</param>
        /// <returns></returns>
        public PfsPdfFile GetPdfFileByLocalName(string localName)
        {
            return PdfFiles
                .Where(p => p.LocalName == localName)
                .FirstOrDefault();
        }
    }
}
