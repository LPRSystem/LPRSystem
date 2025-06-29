using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public interface IGetParkingTicketsRepository
    {

        Task<IEnumerable<Models.ParkingTicket.ParkingTicketModel>> ExecuteAsync();

    }
}
