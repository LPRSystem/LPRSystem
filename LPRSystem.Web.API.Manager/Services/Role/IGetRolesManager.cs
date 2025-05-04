namespace LPRSystem.Web.API.Manager.Services.Role
{
    public interface IGetRolesManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Role.Role>> ExecuteAsync();
    }
}
