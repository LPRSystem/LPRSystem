using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Location
{
    public class GetLocationManager : IGetLocationManager
    {
        private static IGetLocationRepository _repository;
        public GetLocationManager(IGetLocationRepository repository) 
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.Location.LocationModel>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
