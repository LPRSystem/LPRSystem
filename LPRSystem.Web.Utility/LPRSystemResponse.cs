using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.Utility
{
    public class LPRSystemResponse<T>
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
