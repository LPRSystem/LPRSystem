using LPRSystem.Web.API.Manager.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUsersRepository : BaseRepository, IGetUsersRepository
    {
        public GetUsersRepository(IConfiguration configuration) : base(configuration)
        {
                
        }
        public async Task<IEnumerable<Models.User.User>> ExecuteAsync()
        {
            return await base.QueryAsync<Models.User.User>(CommonConstants.CommonDB, UserConstants.GetUsers, null, null);

        }
    }
}
