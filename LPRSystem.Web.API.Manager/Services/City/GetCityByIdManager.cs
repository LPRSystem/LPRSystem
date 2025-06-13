using LPRSystem.Web.API.Manager.Models.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class GetCityByIdManager : IGetCityByIdManager
    {
        private readonly IGetCityByIdRepository _repository;
        public GetCityByIdManager(IGetCityByIdRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Models.City.City> ExecuteAsync(GetCityByIdRequest request)
        {
            return await _repository.ExecuteAsync(request);
        }
    }
}
