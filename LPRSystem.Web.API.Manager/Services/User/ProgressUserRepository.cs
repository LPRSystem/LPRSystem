using Dapper;
using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Converters;
using LPRSystem.Web.API.Manager.Models.Organization;
using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Models.User;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class ProgressUserRepository : BaseRepository, IProgressUserRepository
    {
        public ProgressUserRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<Models.User.User> ExecuteAsync(Models.User.User user)
        {
            string userId = "1111";

            return await base.QueryFirstOrDefaultAsync<Models.User.User>(CommonConstants.CommonDB, UserConstants.InsertOrUpdateUser, new { User = user.ToDataTable(userId).AsTableValuedParameter("[api].[User]") }, null);


        }
    }
}
