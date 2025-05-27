using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.User
{
    public interface IInsertOrUpdateUserManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.User.User>> ExecuteAsync(ProcessUserRequest user);

    }
}
