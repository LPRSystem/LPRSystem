using LPRSystem.Web.API.Manager.Models.City;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public interface IGetCityByIdManager
    {
        Task<Models.City.City> ExecuteAsync(GetCityByIdRequest request);

    }
}
