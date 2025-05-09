using LPRSystem.Web.API.Manager.Constants;
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
    public class GetUserByIdRepository : BaseRepository, IGetUserByIdRepository
    {
        public GetUserByIdRepository(IConfiguration configuration): base(configuration)
        {

        }
        public async Task<Models.User.User> ExecuteAsync(GetUserByIdRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
