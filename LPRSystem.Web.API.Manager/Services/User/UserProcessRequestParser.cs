using LPRSystem.Web.API.Manager.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class UserProcessRequestParser : IRequestParser<LPRSystem.Web.API.Manager.Models.User.User>
    {
        public async Task<Models.User.User> ParseAsync(HttpRequest request)
        {
            string requestBody = await new StreamReader(request.Body).ReadToEndAsync();

            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.User.UserProcessRequest>(requestBody);

            string passwordHash = string.Empty;
            string passwordSalt = string.Empty;

            if (!string.IsNullOrEmpty(requestModel.Password))
            {
                HashSalt hashSalt = HashSalt.GenerateSaltedHash(requestModel.Password);
                passwordHash = hashSalt.Hash;
                passwordSalt = hashSalt.Salt;
            }

            var user = new Models.User.User
            {
                Id = requestModel.Id ?? 0, // Assuming 0 is a default value for Id
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Phone = requestModel.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = requestModel.RoleId,
                IsBlocked = requestModel.IsBlocked,
                LastPasswordChangedOn = requestModel.LastPasswordChangedOn,
                CreatedBy = requestModel.CreatedBy,
                CreatedOn = requestModel.CreatedOn,
                ModifiedBy = requestModel.ModifiedBy,
                ModifiedOn = requestModel.ModifiedOn,
                IsActive = requestModel.IsActive ?? true // Assuming true is the default value for IsActive
            };

            return user; // Return the user object directly
        }
    }
}
