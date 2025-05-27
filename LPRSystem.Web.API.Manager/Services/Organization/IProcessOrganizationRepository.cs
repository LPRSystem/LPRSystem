
using LPRSystem.Web.API.Manager.Models.Organization;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public interface IProcessOrganizationRepository
    {
        Task<LPRSystem.Web.API.Manager.Models.Organization.Organization> ExecuteAsync(LPRSystem.Web.API.Manager.Models.Organization.Organization organization);
    }
}
