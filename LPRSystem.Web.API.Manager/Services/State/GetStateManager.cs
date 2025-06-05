using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.State
{
   public class GetStateManager : IGetStateManager
    {
        private static IGetStateRepository _repository;
        public GetStateManager(IGetStateRepository repository) 
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.State.State>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
