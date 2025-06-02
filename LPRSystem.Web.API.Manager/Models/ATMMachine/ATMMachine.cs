namespace LPRSystem.Web.API.Manager.Models.ATMMachine
{
    public class ATMMachine
    {
        public long ATMId { get; set; }
        public string? ATMCode { get; set; }
        public long? LocationId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
