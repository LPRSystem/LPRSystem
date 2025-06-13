using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface ICityService
    {
        Task<List<CityVM>> GetCitiesAsync();
        Task<List<City>> GetCityAsync();
        Task<City> GetCityByIdAsync(long cityid);
        Task<City> InsertOrUpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(long cityid);
    }
}
