using LPRSystem.Web.API.Manager.Models.Organization;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public interface IProcessOrganizationManager
    {
        Task<LPRSystem.Web.API.Manager.Models.Organization.Organization> ExecuteAsync(Models.Organization.Organization organization);
    }
}
