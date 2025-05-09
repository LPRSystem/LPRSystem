using LPRSystem.Web.API.Manager.Models.User;
using LPRSystem.Web.API.Manager.Services.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public class GetUserByIdManager : IGetUserByIdManager
    {
        public Task<Models.User.User> ExecuteAsync(GetUserByIdRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
