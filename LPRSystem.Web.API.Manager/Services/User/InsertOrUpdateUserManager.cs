using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Models.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class InsertOrUpdateUserManager : BaseRepository, IInsertOrUpdateUserManager
    {
        public InsertOrUpdateUserManager(IConfiguration configuration) : base (configuration) 
        {
            
        }

        public async Task<IEnumerable<Models.User.User>> ExecuteAsync(ProcessUserRequest request)
        {
            return await base.QueryAsync<Models.User.User>(CommonConstants.CommonDB, UserConstants.InsertOrUpdateUser, new { firstName = request.FirstName,lastName= request.LastName, email = request.Email, phone = request.Phone, roleId = request.RoleId, password = request.Password, passwordLastChangeOn = request.PasswordlastChangedOn, isBlocked = request.IsBlocked,  createdBy = request.CreatedBy, createdOn = request.CreatedOn, modifiedBy = request.ModifiedBy, modifiedOn = request.ModifiedOn, isActive = request.IsActive }, null);
        }

    }
}
