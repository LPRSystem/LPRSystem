namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class GetVehiclesManager :IGetVehiclesManager
    {
        private static IGetVehiclesRepository _getVehiclesRepository;
        public GetVehiclesManager(IGetVehiclesRepository getVehiclesRepository)
        {
            _getVehiclesRepository = getVehiclesRepository;
        }
        public async Task<IEnumerable<Models.Vehicles.Vehicles>> ExecuteAsync()
        {
            return await _getVehiclesRepository.ExecuteAsync();
        }
    }
}
