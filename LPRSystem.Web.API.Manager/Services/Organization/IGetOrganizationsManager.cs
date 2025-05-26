namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public interface IGetOrganizationsManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.Organization.Organization>> ExecuteAsync();
    }
}
