using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfFormTestTask.Client
{
    /// <summary>
    /// SErvice Response 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PfsResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool IsOk { get; set; }

        public PfsResponse() { }

        public PfsResponse(string ErrorMessage)
        {
            IsOk = false;
            Message = ErrorMessage;
        }

        public PfsResponse(string message, bool isOk)
        {
            IsOk = isOk;
            Message = message;
        }

        public PfsResponse(T data)
        {
            IsOk = true;
            Data = data;
        }
    }
}
