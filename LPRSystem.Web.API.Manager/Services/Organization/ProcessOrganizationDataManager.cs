using LPRSystem.Web.API.Manager.Models.Organization;

namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public class ProcessOrganizationDataManager : IProcessOrganizationManager
    {
        private static IProcessOrganizationRepository _repository;
        public ProcessOrganizationDataManager(IProcessOrganizationRepository repository)
        {
            _repository = repository;
        }
        public async Task<Models.Organization.Organization> ExecuteAsync(Models.Organization.Organization organization)
        {
            return await _repository.ExecuteAsync(organization);
        }
    }
}
