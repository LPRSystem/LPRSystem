using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public class GetATMMAchineRepository : BaseRepository, IGetATMMAchineRepository
    {
        public GetATMMAchineRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<IEnumerable<Models.ATMMachine.ATMMachine>> ExecuteAsync()
        {

            return await base.QueryAsync<Models.ATMMachine.ATMMachine>(CommonConstants.CommonDB, ATMMachineConstants.GetATMMachine, null, null);

        }
    }
}
