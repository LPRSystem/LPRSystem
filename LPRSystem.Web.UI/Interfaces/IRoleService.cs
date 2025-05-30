using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();

        Task<string> InsertOrUpdateRole(RoleManagement role);
        Task<bool> DeleteRoleAsync(Guid roleId);
    }
}
