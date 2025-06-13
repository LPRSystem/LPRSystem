namespace LPRSystem.Web.API.Manager.Services.City
{
    public interface IGetCitiesManager
    {
        Task<IEnumerable<Models.City.CityModel>> ExecuteAsync();
    }
}
