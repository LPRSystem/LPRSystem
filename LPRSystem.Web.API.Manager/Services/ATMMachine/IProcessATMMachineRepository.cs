using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public interface IProcessATMMachineRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.ATMMachine.ATMMachine> ExecuteAsync(LPRSystem.Web.API.Manager.Models.ATMMachine.ATMMachine atmmachine);

    }
}
