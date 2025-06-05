using LPRSystem.Web.API.Manager.Services.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public class GetATMMachineManager : IGetATMMachineManager
    {
        private static IGetATMMAchineRepository _repository;
        public GetATMMachineManager(IGetATMMAchineRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.ATMMachine.ATMMachinesData>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
