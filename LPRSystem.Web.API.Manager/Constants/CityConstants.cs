using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
   

    public static class CityConstants
    {
        public static string GetCities => new string("[api].[uspGetCities]");
        public static string GetCity => new string("[api].[uspGetCity]");
        public static string InsertOrUpdateCity => new string("[api].[uspInsertOrUpdateCity]");

        public static string GetCityById => new string("[api].[uspGetCityById]");
    }
}
