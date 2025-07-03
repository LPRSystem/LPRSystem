using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Constants
{
    public class ParkingTicketConstants
    {
        public static string GetAllParkingTickets => new string("[api].[uspGetParkingTicket]");
        public static string GetAllParkingTicketsByATM => new string("[api].[uspGetAllParkingTicketsByATM]");
        public static string GetALllParkingTicketsBy => new string("[api].[uspGetALllParkingTicketsBy]");
        public static string GetAllParkingTicketByVehcile => new string("[api].[uspGetAllParkingTicketByVehcile]");
        public static string GetParkingTicketByTicketId => new string("[api].[uspGetParkingTicketByTicketId]");
        public static string InsertParkingTicket => new string("[api].[uspInsertParkingTicket]");
        public static string ExtensParkingTicket => new string("[api].[uspExtensParkingTicket]");
        public static string GetAllParkingTicketByATMIdAndParkedOn => new string("[api].[uspGetAllParkingTicketsByATMIdParkedOn]");

    }
}
