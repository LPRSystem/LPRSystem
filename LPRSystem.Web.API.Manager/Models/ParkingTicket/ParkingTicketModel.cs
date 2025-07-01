using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Models.ParkingTicket
{
    public class ParkingTicketModel
    {
        public long ParkingTicketId { get; set; }
        public long? ATMId { get; set; }
        public string? ATMCode { get; set; }
        public string? ParkingTicketCode { get; set; }
        public string? ParkingTicketRefrence { get; set; }
        public DateTimeOffset? ParkedOn { get; set; }
        public DateTimeOffset? ParkingDurationFrom { get; set; }
        public DateTimeOffset? ParkingDurationTo { get; set; }
        public long? TotalDuration { get; set; }
        public long? ParkingPriceId { get; set; }
        public decimal? Price { get; set; }
        public string? VehicleNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsExtended { get; set; }
        public DateTimeOffset? ExtendedOn { get; set; }
        public DateTimeOffset? ExtendedDurationFrom { get; set; }
        public DateTimeOffset? ExtendedDurationTo { get; set; }
        public decimal? ActualAmount { get; set; }
        public decimal? ExtendedAmount { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Status { get; set; }
        public long? CreatedBy { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
