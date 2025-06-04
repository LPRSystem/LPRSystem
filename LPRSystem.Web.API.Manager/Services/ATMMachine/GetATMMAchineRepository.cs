using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.ATMMachine
{
    public class GetATMMAchineRepository : BaseRepository, IGetATMMAchineRepository
    {
        public GetATMMAchineRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<IEnumerable<Models.ATMMachine.ATMMachinesData>> ExecuteAsync()
        {

            return await base.QueryAsync<Models.ATMMachine.ATMMachinesData>(CommonConstants.CommonDB, ATMMachineConstants.GetATMMachinesData, null, null);

        }
    }
}
