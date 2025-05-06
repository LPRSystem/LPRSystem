using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Role;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class RoleProcessRepository : BaseRepository, IRoleProcessRepository
    {
        public RoleProcessRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Models.Role.Role>> ExecuteAsync(RoleProcessRequest request)
        {
            return await base.QueryAsync<Models.Role.Role>(CommonConstants.CommonDB, RoleConstants.InsertRole, new { name = request.Name, code = request.Code, createdBy = request.CreatedBy, createdOn = request.CreatedOn, modifiedBy = request.ModifiedBy, modifiedOn = request.ModifiedOn, isActive = request.IsActive }, null);
        }
    }
}
