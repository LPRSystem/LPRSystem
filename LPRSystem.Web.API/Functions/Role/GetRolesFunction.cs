using LPRSystem.Web.API.Functions.Role;
using LPRSystem.Web.API.Manager.Services.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LPRSystem.Web.API
{
    public class GetRolesFunction
    {
        private readonly ILogger<GetRoleByIdFunction> _logger;
        private readonly IGetRolesManager _manager;

        public GetRolesFunction(ILogger<GetRoleByIdFunction> logger, IGetRolesManager manager)
        {
            _logger = logger;
            _manager = manager;
        }
        [Function("GetRoles")]
        public async Task<IActionResult> GetRoles([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var response = await _manager.ExecuteAsync();

                return new OkObjectResult(response);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error parsing the request body.");
                return new BadRequestObjectResult("Invalid JSON in request body.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
