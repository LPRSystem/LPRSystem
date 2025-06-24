using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.City;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCitiesRepository : BaseRepository, IGetCitiesRepository
    {
        public GetCitiesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Models.City.CityModel>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.City.CityModel>(CommonConstants.CommonDB, CityConstants.GetCities, null, null);
        }
    }
}
