using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.State
{
   public interface IGetStateRepository
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.State.State>> ExecuteAsync();


    }
}
