using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface ILocationService
    {
        Task<List<LocationVM>> GetLocationsAsync();
        Task<Location> InsertOrUpdateLocation(Location location);

    }
}
