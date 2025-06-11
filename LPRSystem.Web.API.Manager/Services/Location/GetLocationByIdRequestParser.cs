using LPRSystem.Web.API.Manager.Models.Location;
using LPRSystem.Web.API.Manager.Models.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class GetLocationByIdRequestParser : IRequestParser<GetLocationByIdRequest>
    {
        public Task<GetLocationByIdRequest> ParseAsync(HttpRequest request)
        {
            var locationId = request.Query["locationId"].ToString();

            var requestModel = new GetLocationByIdRequest
            {
                LocationId = !string.IsNullOrEmpty(locationId) ? Convert.ToInt64(locationId) : 0,
            };
            return Task.FromResult(requestModel);
        }

    }
}
