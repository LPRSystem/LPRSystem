using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.Utility
{
    public class LPRSystemConfig
    {
        public string ApplicationName { get; set; }
        public string Version { get; set; }
        public string BaseUrl { get; set; }
        public string RedirectUri { get; set; }
    }
}
