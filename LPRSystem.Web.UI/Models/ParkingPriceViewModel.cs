using System.ComponentModel.DataAnnotations;

namespace LPRSystem.Web.UI.Models
{
    public class ParkingPriceViewModel
    {
        public long ParkingPriceId { get; set; }
        [Required(ErrorMessage = "Duration is required")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }

}
