using LPRSystem.Web.API.Manager.Models.City;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCitiesManager : IGetCitiesManager
    {
        private static IGetCitiesRepository _repository;
        public GetCitiesManager(IGetCitiesRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CityModel>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
