using LPRSystem.Web.API.Manager.Models.City;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCityByIdRequestParser : IRequestParser<GetCityByIdRequest>
    {
        public Task<GetCityByIdRequest> ParseAsync(HttpRequest request)
        {
            var cityId = request.Query["cityId"].ToString();
            var requestModel = new GetCityByIdRequest
            {
                CityId = !string.IsNullOrEmpty(cityId) ? Convert.ToInt32(cityId) : 0,
            };
            return Task.FromResult(requestModel);
        }
    }
}
