
using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class GetRolesRepository : BaseRepository, IGetRolesRepository
    {
        public GetRolesRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Models.Role.Role>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.Role.Role>(CommonConstants.CommonDB, RoleConstants.GetRoles, null, null);
        }
    }
} 
