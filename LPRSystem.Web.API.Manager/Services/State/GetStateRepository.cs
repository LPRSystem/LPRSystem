using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.State
{
   public class GetStateRepository : BaseRepository, IGetStateRepository
    {
        public GetStateRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IEnumerable<Models.State.State>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.State.State>(CommonConstants.CommonDB, StateConstants.GetState, null, null);
        }
    }
}
