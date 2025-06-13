using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.City;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCityByIdRepository :BaseRepository , IGetCityByIdRepository
    {
        public GetCityByIdRepository(IConfiguration configuration) : base(configuration)
        {
            
        }

        public async Task<Models.City.City> ExecuteAsync(GetCityByIdRequest request)
        {
            return await base.QueryFirstOrDefaultAsync<Models.City.City>(CommonConstants.CommonDB, CityConstants.GetCityById, new { cityId = request.CityId }, null);
        }
    }
}
