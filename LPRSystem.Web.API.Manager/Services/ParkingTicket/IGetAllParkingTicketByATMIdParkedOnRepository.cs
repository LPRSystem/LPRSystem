using LPRSystem.Web.API.Manager.Models.ParkingTicket;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public interface IGetAllParkingTicketByATMIdParkedOnRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket> ExecuteAsync(GetAllParkingTicketByATMIdParkedOnRequest aTMIdRequest);

    }
}
