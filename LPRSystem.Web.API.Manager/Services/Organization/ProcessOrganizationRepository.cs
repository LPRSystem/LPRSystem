
using Dapper;
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Converters;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public class ProcessOrganizationRepository : BaseRepository, IProcessOrganizationRepository
    {
        public ProcessOrganizationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<Models.Organization.Organization> ExecuteAsync(Models.Organization.Organization organization)
        {
            string userId = "1111";

            return await base.ExecuteScalarAsync<Models.Organization.Organization>(CommonConstants.CommonDB, OrganizationConstants.InsertOrUpdateOrganization, new { Organization = organization.ToDataTable(userId).AsTableValuedParameter("[api].[Organization]") }, null);
        }
    }
}
