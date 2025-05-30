
using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public class GetOrganizationsRepository : BaseRepository, IGetOrganizationsRepository
    {
        public GetOrganizationsRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Models.Organization.Organization>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.Organization.Organization>(CommonConstants.CommonDB, OrganizationConstants.GetOrganizations, new { }, null);
        }
    }
}
