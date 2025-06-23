using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace LPRSystem.Web.UI.Factory
{
    public class UserPrincipal
    {
        public static ClaimsPrincipal GenarateUserPrincipal(ApplicationUser user)
        {

            var claims = new List<Claim>
              {
                 new Claim("Id", user.Id.ToString()),
                 new Claim("Phone", user.Phone),
                 new Claim("Email", user.Email),
                 new Claim("FullName", user.FullName),
                 new Claim("FirstName", user.FirstName),
                 new Claim("LastName", user.LastName),
                 new Claim("RoleId", user.RoleId.ToString()),
              };

            var principal = new ClaimsPrincipal();

            principal.AddIdentity(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

            return principal;
        }
    }
}
