using LPRSystem.Web.API.Manager.Models.Role;
using Microsoft.AspNetCore.Http;

namespace LPRSystem.Web.API.Manager.Services.Role
{
    public class GetRoleByIdRequestParser : IRequestParser<GetRoleByIdRequest>
    {
        public Task<GetRoleByIdRequest> ParseAsync(HttpRequest request)
        {
            var roleId = request.Query["roleId"].ToString();

            var requestModel = new GetRoleByIdRequest
            {
                RoleId = !string.IsNullOrEmpty(roleId) ? Convert.ToInt64(roleId) : 0,
            };
            return Task.FromResult(requestModel);
        }
    }
}
