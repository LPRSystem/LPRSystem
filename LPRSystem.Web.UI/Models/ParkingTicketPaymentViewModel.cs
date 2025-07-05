namespace LPRSystem.Web.UI.Models
{
    public class ParkingTicketPaymentViewModel
    {
        public long ParkingTicketPaymentId { get; set; }
        public long ATMId { get; set; }
        public long PaymentMethodId { get; set; }
        public long ParkingTicketId { get; set; }
        public string? PaymentReference { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? Status { get; set; }
    }
}
