using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Services;
using LPRSystem.Web.API.Manager.Services.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.API.Functions.Role
{
    public class GetRoleByIdFunction
    {
        private readonly ILogger<GetRoleByIdFunction> _logger;
        private readonly IGetRoleByIdManager _manager;
        private readonly IRequestParser<GetRoleByIdRequest> _requestParser;

        public GetRoleByIdFunction(
            ILogger<GetRoleByIdFunction> logger,
            IGetRoleByIdManager manager,
            IRequestParser<GetRoleByIdRequest> requestParser)
        {
            _logger = logger;
            _manager = manager;
            _requestParser = requestParser;
        }

        [Function("GetRoleById")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                // Use the parser to parse the request
                var request = await _requestParser.ParseAsync(req);

                // Validate the id (you might want to move this validation to the parser)
                if (request.RoleId <= 0)
                {
                    _logger.LogWarning("Invalid roleId provided: {RoleId}", request.RoleId);
                    return new BadRequestObjectResult("Invalid roleId parameter.");
                }

                // Execute the manager method
                var response = await _manager.ExecuteAsync(request);

                // Check if the response is null or indicates an error
                if (response == null)
                {
                    _logger.LogWarning("No role found for roleId: {RoleId}", request.RoleId);
                    return new NotFoundObjectResult($"No role found for roleId: {request.RoleId}");
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
