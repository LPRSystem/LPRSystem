using LPRSystem.Web.API.Manager.Models.Role;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public interface IGetRoleByIdRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.Role.Role> ExecuteAsync(GetRoleByIdRequest request);
    }
}
