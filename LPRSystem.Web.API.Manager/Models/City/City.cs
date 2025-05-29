using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LPRSystem.Web.API.Manager.Models.City
{
   public class City
    {
        public long CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CityCode { get; set; }

    }
}
