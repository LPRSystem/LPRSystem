using LPRSystem.Web.API.Manager.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class VehiclesProcessManager :IVehiclesProcessManager
    {
        private static IVehiclesProcessRepository vehiclesProcessRepository;
        public VehiclesProcessManager(IVehiclesProcessRepository _vehiclesProcessRepository)
        {
            _vehiclesProcessRepository = vehiclesProcessRepository;
        }
        public async Task<IEnumerable<Models.Vehicles.Vehicles>> ExecuteAsync(VehicleProcessRequest request)
        {
            return await vehiclesProcessRepository.ExecuteAsync(request);
        }
    }
}
