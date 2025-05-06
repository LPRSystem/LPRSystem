using LPRSystem.Web.API.Manager.Models.Role;
using System.Diagnostics.CodeAnalysis;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class RoleProcessManager : IRoleProcessManager
    {
        private static IRoleProcessRepository _roleProcessRepository;
        public RoleProcessManager(IRoleProcessRepository roleProcessRepository)
        {
            _roleProcessRepository = roleProcessRepository;
        }
        public async Task<IEnumerable<Models.Role.Role>> ExecuteAsync(RoleProcessRequest request)
        {
            return await _roleProcessRepository.ExecuteAsync(request);
        }
    }
}
