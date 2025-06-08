namespace LPRSystem.Web.API.Manager.Models.ParkingPlace
{
    public class ParkingPlace
    {
        public long ParkingPlaceId { get; set; }
        public string? ParkingPlaceName { get; set; }
        public string? ParkingPlaceCode { get; set; }
        public string? ParkingPlaceType { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
