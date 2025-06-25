using System.ComponentModel.DataAnnotations;

namespace LPRSystem.Web.UI.Models
{
    public class ParkingPrice
    {
      
        public long ParkingPriceId { get; set; }
        [Required]
        public string? Duration { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
