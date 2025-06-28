using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetParkingTicketByIATMdParkedOnRequestParser : IRequestParser<GetAllParkingTicketByATMIdParkedOnRequest>
    {
        public Task<GetAllParkingTicketByATMIdParkedOnRequest> ParseAsync(HttpRequest request)
        {
            var atmId = request.Query["atmId"].ToString();
            var parkedOn = request.Query["parkedOn"].ToString().ToLower();
            var requestModel = new GetAllParkingTicketByATMIdParkedOnRequest
            {
                ATMId = !string.IsNullOrEmpty(atmId) ? Convert.ToInt32(atmId) : 0,
                ParkedOn = !string.IsNullOrEmpty(parkedOn)? Convert.ToDateTime(parkedOn) : null, 
            };
            return Task.FromResult(requestModel);
        }
    }
}
