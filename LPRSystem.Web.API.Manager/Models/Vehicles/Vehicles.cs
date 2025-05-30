    namespace LPRSystem.Web.API.Manager.Models.Vehicles
{
    public class Vehicles
    {
        public long VehicleId { get; set; }
        public string? VehiclesName { get; set; }
        public string? VehicleType { get; set;}
        public string? LicencePlate { get; set; }
        public string? OwnerName { get; set; }
        public string? ContactNumber { get; set; }
        public long? CreatedBy {  get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
