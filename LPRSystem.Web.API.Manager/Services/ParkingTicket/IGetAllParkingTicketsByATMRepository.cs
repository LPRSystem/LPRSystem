using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public interface IGetAllParkingTicketsByATMRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket> ExecuteAsync(GetParkingTicketByATMIdRequest atmId);
    }
}
