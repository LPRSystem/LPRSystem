using LPRSystem.Web.API.Manager.Models.User;
using LPRSystem.Web.API.Manager.Services.Role;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUserByIdManager : IGetUserByIdManager
    {
        private static IGetUserByIdRepository _repository;
        public GetUserByIdManager(IGetUserByIdRepository repository)
        {
            _repository = repository;
        }
        public async Task<Models.User.User> ExecuteAsync(GetUserByIdRequest request)
        {
            return await _repository.ExecuteAsync(request);
        }
    }
}
