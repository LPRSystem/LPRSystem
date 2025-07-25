﻿namespace LPRSystem.Web.UI.Models
{
    public class City
    {
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CityCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
