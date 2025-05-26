
namespace LPRSystem.Web.API.Manager.Services.Organization
{
    public class GetOrganizationsDataManager : IGetOrganizationsManager
    {
        private static IGetOrganizationsRepository _repository;
        public GetOrganizationsDataManager(IGetOrganizationsRepository repository) { _repository = repository; }
        public async Task<IEnumerable<Models.Organization.Organization>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
