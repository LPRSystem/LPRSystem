using LPRSystem.Web.API.Manager.Models.City;
using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingTicket
{
    public class GetParkingTicketByIATMdRequestParser : IRequestParser<GetParkingTicketByATMIdRequest>
    {
        public Task<GetParkingTicketByATMIdRequest> ParseAsync(HttpRequest request)
        {
            var atmId = request.Query["atmId"].ToString();
            var requestModel = new GetParkingTicketByATMIdRequest
            {
                ATMId = !string.IsNullOrEmpty(atmId) ? Convert.ToInt32(atmId) : 0,
            };
            return Task.FromResult(requestModel);
        }

      
    }
}
