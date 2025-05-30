using LPRSystem.Web.API.Manager.Models.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public interface IVehiclesProcessManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Vehicles.Vehicles>> ExecuteAsync(VehicleProcessRequest request);
    }
}
