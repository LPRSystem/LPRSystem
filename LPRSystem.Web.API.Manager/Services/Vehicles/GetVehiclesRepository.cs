using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Services.Role;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class GetVehiclesRepository : BaseRepository, IGetVehiclesRepository
    {
        public GetVehiclesRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task<IEnumerable<Models.Vehicles.Vehicles>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.Vehicles.Vehicles>(CommonConstants.CommonDB, VehiclesConstants.GetVehicles, null, null);
        }
    }
}
