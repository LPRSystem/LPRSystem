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
    public class ProgressUserDataManager : IProgressUserDataManager
    {
        private static IProgressUserRepository _repository; 
        public ProgressUserDataManager(IProgressUserRepository repository) 
        {
            _repository = repository;
        }

        public async Task<Models.User.User> ExecuteAsync(Models.User.User user)
        {
            return await _repository.ExecuteAsync(user);
        }

    }
}
