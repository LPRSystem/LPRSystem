using LPRSystem.Web.API.Manager.Models.ParkingTicket;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public interface IGetAllParkingTicketsByATMManager
    {
        Task<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket> ExecuteAsync(Models.ParkingTicket.GetParkingTicketByATMIdRequest atmId);
    }
}
