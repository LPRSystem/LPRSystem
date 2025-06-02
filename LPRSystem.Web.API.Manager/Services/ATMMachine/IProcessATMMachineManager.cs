using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public interface IProcessATMMachineManager
    {
        Task<LPRSystem.Web.API.Manager.Models.ATMMachine.ATMMachine> ExecuteAsync(Models.ATMMachine.ATMMachine atmmachine);

    }
}
