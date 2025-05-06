
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Role;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class GetRoleByIdRepository : BaseRepository, IGetRoleByIdRepository
    {
        public GetRoleByIdRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Models.Role.Role> ExecuteAsync(GetRoleByIdRequest request)
        {
            return await base.QueryFirstOrDefaultAsync<Models.Role.Role>(CommonConstants.CommonDB, RoleConstants.GetRoleById, new { roleId = request.RoleId }, null);
        }
    }
}
