using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Services.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Functions.Role
{
    public class GetRoleById
    {
        private readonly ILogger<GetRoleById> _logger;
        private readonly IGetRoleByIdManager _manager;

        public GetRoleById(ILogger<GetRoleById> logger, IGetRoleByIdManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Function("GetRoleById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Read the roleId from the query parameters
                var id = req.Query["roleId"].ToString();

                // Validate the id
                if (string.IsNullOrEmpty(id) || !long.TryParse(id, out long roleId))
                {
                    _logger.LogWarning("Invalid roleId provided: {RoleId}", id);
                    return new BadRequestObjectResult("Invalid roleId parameter.");
                }

                // Create the request object
                GetRoleByIdRequest request = new GetRoleByIdRequest()
                {
                    RoleId = roleId,
                };

                // Execute the manager method
                var response = await _manager.ExecuteAsync(request);

                // Check if the response is null or indicates an error
                if (response == null)
                {
                    _logger.LogWarning("No role found for roleId: {RoleId}", roleId);
                    return new NotFoundObjectResult($"No role found for roleId: {roleId}");
                }

                // Return the successful response
                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "An error occurred while processing the request.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
