namespace LPRSystem.Web.UI.Models
{
    public class ParkingTicketPayment
    {
        public long ParkingTicketPaymentId { get; set; }
        public long? ATMId { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? ParkingTicketId { get; set; }
        public string? PaymentReference { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal DueAmount { get; set; }
        public string? Status { get; set; }
        public long CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public long ModifiedBy { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
