
namespace LPRSystem.Web.API.Manager.Models
{
    public class ParkingTicket
    {
        public long ParkingTicketId { get; set; }
        public long? ATMId { get; set; }
        public string? ParkingTicketCode { get; set; }
        public string? ParkingTicketRefrence { get; set; }
        public DateTimeOffset? ParkedOn { get; set; }
        public string? ParkingDurationFrom { get; set; }
        public string? ParkingDurationTo { get; set; }
        public DateTimeOffset? TotalDuration { get; set; }
        public long? ParkingPriceId { get; set; }
        public string? vehicleNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsExtended { get; set; }
        public DateTimeOffset? ExtendedOn { get; set; }
        public string? ExtendedDurationFrom { get; set; }
        public string? ExtendedDurationTo { get; set; }
        public Decimal? ActualAmount  { get; set; }
        public String? ExtendeAmount { get; set; }
        public Decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
