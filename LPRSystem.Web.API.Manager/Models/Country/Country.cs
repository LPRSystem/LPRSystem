﻿namespace LPRSystem.Web.API.Manager.Models.Country
{
    public class Country
    {
        public long CountryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CountryCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
