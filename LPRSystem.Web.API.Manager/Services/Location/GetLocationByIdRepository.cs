using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Location;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class GetLocationByIdRepository : BaseRepository, IGetLocationByIdRepository
    {
        public GetLocationByIdRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Models.Location.Location> ExecuteAsync(GetLocationByIdRequest request)
        {
            return await base.QueryFirstOrDefaultAsync<Models.Location.Location>(CommonConstants.CommonDB, LocationConstants.GetLocationById, new { locationId = request.LocationId }, null);

        }
    }
}
