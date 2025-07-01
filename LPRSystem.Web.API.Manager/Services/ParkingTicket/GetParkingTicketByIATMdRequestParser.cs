using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.AspNetCore.Http;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetParkingTicketByIATMdRequestParser : IRequestParser<Models.ParkingTicket.GetParkingTicketByATMIdRequest>
    {
        public Task<Models.ParkingTicket.GetParkingTicketByATMIdRequest> ParseAsync(HttpRequest request)
        {
            var atmId = request.Query["atmId"].ToString();
            var requestModel = new Models.ParkingTicket.GetParkingTicketByATMIdRequest
            {
                ATMId = !string.IsNullOrEmpty(atmId) ? Convert.ToInt32(atmId) : 0,
            };
            return Task.FromResult(requestModel);
        }

      
    }
}
