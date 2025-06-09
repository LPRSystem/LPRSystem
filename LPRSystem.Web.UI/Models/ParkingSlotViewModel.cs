using System.ComponentModel.DataAnnotations;

namespace LPRSystem.Web.UI.Models
{
    public class ParkingSlotViewModel
    {
        public ParkingSlotViewModel()
        {
            parkingPlaces = new List<ParkingPlace>();
            atmMachines = new List<ATMMachine>();
        }
        public long? ParkingSlotId { get; set; }
        public long? ParkingPlaceId { get; set; }
        public long? ATMId { get; set; }
        public string? ParkingSlotCode { get; set; }
        public List<ParkingPlace> parkingPlaces { get; set; }
        public List<ATMMachine> atmMachines { get; set; }
    }
}
