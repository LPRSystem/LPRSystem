using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class ProcessLocationManager : IProcessLocationManager
    {
        private readonly IProcessLocationRepository _repository;
        public ProcessLocationManager(IProcessLocationRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Models.Location.Location> ExecuteAsync(Models.Location.Location location)
        {
            return await _repository.ExecuteAsync(location);
        }
    }
}
