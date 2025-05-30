using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Vehicles;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class GetVehiclesByIdRepository :BaseRepository, IGetVehiclesByIdRepository
    {
        public GetVehiclesByIdRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public async Task <Models.Vehicles.Vehicles> ExecuteAsync(GetVehiclesByIdRequest request)
        {
            return await base.QueryFirstOrDefaultAsync<Models.Vehicles.Vehicles>(CommonConstants.CommonDB, VehiclesConstants.GetVehiclesById, new { VehicleId = request.VehicleId }, null);
        }
    }
}
