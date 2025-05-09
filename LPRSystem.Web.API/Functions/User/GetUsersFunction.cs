using LPRSystem.Web.API.Manager.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;


namespace LPRSystem.Web.API.Functions.User
{
    public class GetUsersFunction
    {
        private readonly ILogger<GetUserByIdFunction> _logger;
        private readonly IGetUsersManager _manager;

        public GetUsersFunction(ILogger<GetUserByIdFunction> logger, IGetUsersManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Function ("GetUsers")]
        public async Task<IActionResult> GetUsers([HttpTrigger(AuthorizationLevel.Anonymous,"get", Route = "users")] HttpRequest req, ILogger logger)
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
