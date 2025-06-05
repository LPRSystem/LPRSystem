using LPRSystem.Web.API.Manager.Services.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.Country
{
   public class GetCountryManager : IGetCountryManager
    {
        private static IGetCountryRepository _repository;
        public GetCountryManager(IGetCountryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.Country.Country>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
