using LPRSystem.Web.API.Manager.Services.Organization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.API;

public class GetOrganizationsFunction
{
    private readonly ILogger<GetOrganizationsFunction> _logger;
    private readonly IGetOrganizationsManager _manager;
    public GetOrganizationsFunction(ILogger<GetOrganizationsFunction> logger,
        IGetOrganizationsManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetOrganizations")]
    public async Task<IActionResult> GetOrganizations([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "organizations/getallorganizations")] HttpRequest req)
    {
        _logger.LogInformation("GetOrganizationsFunction.Invoked a request.");

        try
        {
            var response = await _manager.ExecuteAsync();

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}