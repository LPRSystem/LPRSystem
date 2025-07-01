using LPRSystem.Web.API.Manager.Models.ParkingTicket;
namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public interface IGetAllParkingTicketByATMIdParkedOnManager
    {
        Task<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket> ExecuteAsync(Models.ParkingTicket.GetAllParkingTicketByATMIdParkedOnRequest aTMIdRequest);
    }
}
