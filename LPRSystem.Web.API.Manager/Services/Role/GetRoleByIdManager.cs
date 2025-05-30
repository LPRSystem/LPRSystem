    
using LPRSystem.Web.API.Manager.Models.Role;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class GetRoleByIdManager : IGetRoleByIdManager
    {
        private static IGetRoleByIdRepository _getRoleByIdRepository;
        public GetRoleByIdManager(IGetRoleByIdRepository getRoleByIdRepository)
        {
            _getRoleByIdRepository = getRoleByIdRepository;
        }
        public async Task<Models.Role.Role> ExecuteAsync(GetRoleByIdRequest request)
        {
            return await _getRoleByIdRepository.ExecuteAsync(request);
        }
    }
}
