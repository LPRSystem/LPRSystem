using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> FetchAllUser();
        Task<string> InsertOrUpdateUser(UserRegistration user);
    }
}
