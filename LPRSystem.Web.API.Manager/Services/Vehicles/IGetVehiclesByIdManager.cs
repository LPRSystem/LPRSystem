
using LPRSystem.Web.API.Manager.Models.Vehicles;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public interface IGetVehiclesByIdManager
    {
        Task<Models.Vehicles.Vehicles> ExecuteAsync(GetVehiclesByIdRequest request);
    }
}
