using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
    public class LocationConstants
    {
        public static string GetLocations => new string("[api].[uspGetLocation]");

        public static string GetLocationById => new string("[api].[uspGetLocationById]");
        public static string InsertOrUpdateLocation => new string("[api].[uspGetLocation]");


    }
}
