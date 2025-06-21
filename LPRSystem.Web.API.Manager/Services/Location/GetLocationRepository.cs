using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class GetLocationRepository : BaseRepository, IGetLocationRepository
    {
        public GetLocationRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<IEnumerable<Models.Location.LocationModel>> ExecuteAsync()
        {

            return await base.QueryAsync<Models.Location.LocationModel>(CommonConstants.CommonDB, LocationConstants.GetLocations, null, null);

        }
    }
}
