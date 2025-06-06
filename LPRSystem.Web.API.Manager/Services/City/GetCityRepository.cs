using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
   public class GetCityRepository :BaseRepository, IGetCityRepository
    {
        public GetCityRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Models.City.City>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.City.City>(CommonConstants.CommonDB, CityConstants.GetCity, null, null);
        }
    }
}
