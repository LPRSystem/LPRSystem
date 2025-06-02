using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public class ProcessATMMachineManager : IProcessATMMachineManager
    {
        private static IProcessATMMachineRepository _repository;
        public ProcessATMMachineManager(IProcessATMMachineRepository repository) 
        { 
            _repository = repository;
        }
        public async  Task<Models.ATMMachine.ATMMachine> ExecuteAsync(Models.ATMMachine.ATMMachine atmmachine)
        {
            return await _repository.ExecuteAsync(atmmachine);
        }
    }
}
