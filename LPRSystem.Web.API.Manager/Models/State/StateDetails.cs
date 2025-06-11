using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Models.State
{
    public class StateDetails : State
    {
        public string? Name { get; set; }
        public string? CountryCode { get; set; }
    }
}
