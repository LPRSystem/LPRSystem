namespace LPRSystem.Web.UI.Models
{
    public class Location
    {
        public long? LocationId { get; set; }
        public string? LocationName { get; set; }
        public string? Code { get; set; }
        public string? Address { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
