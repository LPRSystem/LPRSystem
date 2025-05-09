
using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Models.User;
using Microsoft.AspNetCore.Http;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUserByIdRequestParser : IRequestParser<GetUserByIdRequest>
    {
        public Task<GetUserByIdRequest> ParseAsync(HttpRequest request)
        {
            var userId = request.Query["userId"].ToString();

            var requestModel = new GetUserByIdRequest
            {
                UserId = !string.IsNullOrEmpty(userId) ? Convert.ToInt64(userId) : 0,
            };
            return Task.FromResult(requestModel);
        }
    }
}
