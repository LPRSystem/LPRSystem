using System.ComponentModel.DataAnnotations;

namespace LPRSystem.Web.UI.Models
{
    public class ParkingPriceViewModel
    {
        public long ParkingPriceId { get; set; }
        [Required]
        public string? Duration { get; set; }
        [Required]
        public decimal? Price { get; set; }
        
       
    }
}
