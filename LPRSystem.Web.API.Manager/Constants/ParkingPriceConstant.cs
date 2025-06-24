using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
    public class ParkingPriceConstant
    {
        public static string GetParkingPrice => new string("[api].[uspGetParkingPrice]");
        public static string GetParkingPriceById => new string("[api].[uspGetParkingPriceById]");
        public static string SaveParkingPrice => new string("[api].[uspSaveParkingPrice]");

        public static string UpdateParkingPrice => new string("[api].[uspUpdateParkingPrice]");
    }
}
