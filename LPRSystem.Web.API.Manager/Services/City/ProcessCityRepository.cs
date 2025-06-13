using Dapper;
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Converters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class ProcessCityRepository :BaseRepository, IProcessCityRepository
    {
        public ProcessCityRepository(IConfiguration configuration): base(configuration) 
        { }

        public async Task<Models.City.City> ExecuteAsync(Models.City.City city)
        {
            return await base.QueryFirstOrDefaultAsync<Models.City.City>(CommonConstants.CommonDB , CityConstants.InsertOrUpdateCity, new {City = city.ToDataTable().AsTableValuedParameter("[api].[City]")}, null);
        }
    }
}
