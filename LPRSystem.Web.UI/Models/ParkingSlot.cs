namespace LPRSystem.Web.UI.Models
{
    public class ParkingSlot
    {
        public long? ParkingSlotId { get; set; }
        public long? ParkingPlaceId { get; set; }
        public string? ParkingSlotCode { get; set; }
        public long? ATMId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
