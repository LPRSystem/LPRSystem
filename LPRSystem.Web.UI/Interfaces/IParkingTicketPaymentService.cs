using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IParkingTicketPaymentService
    {
        Task<List<ParkingTicketPayment>> GetParkingTicketPaymentAsync();
        Task<ParkingTicketPayment> GetATMByIdAsync(long aTMId);
        Task<ParkingTicketPayment> GetParkingTicketPaymentByIdAsync(long parkingTicketPaymentId);
        Task<long> InsertParkingTicketPaymentAsync(ParkingTicketPayment parkingTicketPayment);
        Task<ParkingTicketPayment> UpdateParkingTicketPaymentAsync(ParkingTicketPayment parkingTicketPayment);
       
    }
}
