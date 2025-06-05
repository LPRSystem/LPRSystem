using LPRSystem.Web.API.Manager.Services.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCityManager : IGetCityManager
    {

        private static IGetCityRepository _repository;
        public GetCityManager(IGetCityRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.City.City>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
