

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public interface IGetVehiclesRepository
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Vehicles.Vehicles>> ExecuteAsync();
    }
}
