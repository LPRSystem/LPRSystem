
using LPRSystem.Web.API.Manager.Models.City;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public interface IGetCitiesRepository
    {
        Task<IEnumerable<Models.City.CityModel>> ExecuteAsync();
    }
}
