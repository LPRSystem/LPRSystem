using LPRSystem.Web.API.Manager.Services.Organization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.Organization;

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

    [Function("GetOrganizationsFunction")]
    public async Task<IActionResult> GetOrganizations([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organizations/getorganizations")] HttpRequest req)
    {
        _logger.LogInformation("GetOrganizationsFunction Invoke().");
        try
        {
            var responce = await _manager.ExecuteAsync();
            return new OkObjectResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the organizations the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}