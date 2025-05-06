using LPRSystem.Web.API.Manager.Models.Role;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public interface IRoleProcessManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Role.Role>> ExecuteAsync(RoleProcessRequest request);
    }
}
