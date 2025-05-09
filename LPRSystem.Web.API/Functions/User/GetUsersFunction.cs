using LPRSystem.Web.API.Functions.Role;
using LPRSystem.Web.API.Manager.Services.Role;
using LPRSystem.Web.API.Manager.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Functions.User
{
    public class GetUsersFunction
    {
        private readonly ILogger<GetRoleByIdFunction> _logger;
        private readonly IGetUsersManager _manager;

        public GetUsersFunction(ILogger<GetRoleByIdFunction> logger, IGetUsersManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Function ("GetUsers")]
        public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Anonymous,"get",Route = null)] HttpRequest req, ILogger logger)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            try
            {
                var responce = await _manager.ExecuteAsync();
                return new OkObjectResult(responce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
