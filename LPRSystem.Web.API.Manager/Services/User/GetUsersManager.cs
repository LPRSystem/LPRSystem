
namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUsersManager : IGetUsersManager
    {
        private static IGetUsersRepository _repository;
        public GetUsersManager(IGetUsersRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Models.User.User>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
