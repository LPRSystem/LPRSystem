using LPRSystem.Web.API.Manager.Models.Vehicles;


namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class GetVehiclesByIdManager :IGetVehiclesByIdManager
    {
        private static IGetVehiclesByIdRepository _getVehiclesByIdRepository;
        public GetVehiclesByIdManager(IGetVehiclesByIdRepository getVehiclesByIdRepository)
        {
            _getVehiclesByIdRepository = getVehiclesByIdRepository;
        }
        public async Task <Models.Vehicles.Vehicles> ExecuteAsync(GetVehiclesByIdRequest request)
        {
            return await _getVehiclesByIdRepository.ExecuteAsync(request);
        }
    }
}
