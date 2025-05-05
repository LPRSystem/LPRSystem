using LPRSystem.Web.API.Manager.Models.Role;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public interface IGetRoleByIdManager
    {
        Task<Models.Role.Role> ExecuteAsync(GetRoleByIdRequest request);
    }
}
