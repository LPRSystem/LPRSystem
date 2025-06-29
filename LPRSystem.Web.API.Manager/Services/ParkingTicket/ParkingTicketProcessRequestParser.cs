using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class ParkingTicketProcessRequestParser : IRequestParser<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket>
    {
        public async Task<Models.ParkingTicket.ParkingTicket> ParseAsync(HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicketProcessRequest>(requestBody);
            var parkingTicket = new Models.ParkingTicket.ParkingTicket
            {
                ParkingTicketId = requestModel.ParkingTicketId ?? 0,// Assuming 0 is a default value for Id
                ATMId = requestModel.ATMId,
                ParkingTicketCode = requestModel.ParkingTicketCode,
                ParkingTicketRefrence = requestModel.ParkingTicketRefrence,
                ParkedOn = requestModel.ParkedOn,
                ParkingDurationFrom = requestModel.ParkingDurationFrom,
                ParkingDurationTo = requestModel.ParkingDurationTo,
                TotalDuration = requestModel.TotalDuration,
                ParkingPriceId = requestModel.ParkingPriceId,
                VehicleNumber = requestModel.VehicleNumber,
                PhoneNumber = requestModel.PhoneNumber,
                IsExtended = requestModel.IsExtended,
                ExtendedOn = requestModel.ExtendedOn,
                ExtendedDurationFrom = requestModel.ExtendedDurationFrom,
                ExtendedDurationTo = requestModel.ExtendedDurationTo,
                ActualAmount = requestModel.ActualAmount,
                ExtendedAmount = requestModel.ExtendedAmount,
                TotalAmount = requestModel.TotalAmount,
                Status = requestModel.Status,
                CreatedBy = requestModel.CreatedBy,
                CreatedOn = requestModel.CreatedOn,
                ModifiedBy = requestModel.ModifiedBy,
                ModifiedOn = requestModel.ModifiedOn,
                IsActive = requestModel.IsActive // Assuming true is the default value for IsActive
            };
            return parkingTicket;
        }
    }
}
