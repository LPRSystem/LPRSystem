using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.City
{
    public class ProcessCityManager : IProcessCityManager
    {
        private readonly IProcessCityRepository _repository;
        public ProcessCityManager(IProcessCityRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Models.City.City> ExecuteAsync(Models.City.City city)
        {
            return await _repository.ExecuteAsync(city);
        }
    }
}
