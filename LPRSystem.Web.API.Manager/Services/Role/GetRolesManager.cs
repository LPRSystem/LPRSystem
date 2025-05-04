namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class GetRolesManager : IGetRolesManager
    {
        private static IGetRolesRepository _getRolesRepository;
        public GetRolesManager(IGetRolesRepository getRolesRepository)
        {
            _getRolesRepository = getRolesRepository;
        }
        public async Task<IEnumerable<Models.Role.Role>> ExecuteAsync()
        {
            return await _getRolesRepository.ExecuteAsync();
        }
    }
}
