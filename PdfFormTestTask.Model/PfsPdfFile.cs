using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Model
{
    /// <summary>
    /// PDF File Model
    /// </summary>
    public class PfsPdfFile
    {
        /// <summary>
        /// File Identifier based on GUID.
        /// Also file name from service side.
        /// </summary>
        public string LocalName { get; set; }

        /// <summary>
        /// Original uploaded file name.
        /// </summary>
        public string FileName { get; set; }
    }
}
