using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IParkingTicketService
    {
        Task<List<ParkingTicket>> GetParkingTicketAsync();
        Task<ParkingTicket> GetParkingTicketByIdAsync(long parkingTicketId);
        Task<long> InserOrUpdateParkingTicketAsync(ParkingTicket parkingTicket);
        Task<ParkingTicket> UpdateParkingticketAsync(ParkingTicket parkingTicket);
        Task<bool> DeleteParkingTicketAsync(long parkingTicketId);
    }
}
