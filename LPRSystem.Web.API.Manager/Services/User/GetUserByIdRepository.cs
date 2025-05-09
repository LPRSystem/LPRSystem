using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.User;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUserByIdRepository : BaseRepository, IGetUserByIdRepository
    {
        public GetUserByIdRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<Models.User.User> ExecuteAsync(GetUserByIdRequest request)
        {
            return await base.QueryFirstOrDefaultAsync<Models.User.User>(CommonConstants.CommonDB, UserConstants.GetUserById, new { userId = request.UserId }, null);
        }
    }
}
