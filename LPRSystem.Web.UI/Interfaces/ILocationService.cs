using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface ILocationService
    {
        Task<List<Location>> GetLocationsAsync();
        Task<string> InsertOrUpdateLocation(Location location);

    }
}
