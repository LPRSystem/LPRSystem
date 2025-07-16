namespace LPRSystem.Web.UI.Models
{
    public class ParkingTicketPaymentViewModel
    {

        public ParkingTicketPaymentViewModel()
        {
            paymentMethod = new List<PaymentMethod>();
            atmMachines = new List<ATMMachine>();
            parkingTicket = new List<ParkingTicket>();
        }
        public long? ParkingTicketPaymentId { get; set; }
        public long? ATMId { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? ParkingTicketId { get; set; }
        public string? PaymentReference { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? Status { get; set; }
        public List<PaymentMethod> paymentMethod { get; set; }
        public List<ATMMachine> atmMachines { get; set; }
        public List<ParkingTicket> parkingTicket { get; set; }
    }
}
