using Azure;
using LPRSystem.Web.API.Manager.Services.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.Location;

public class GetLocationFunction
{
    private readonly ILogger<GetLocationFunction> _logger;
    private readonly IGetLocationManager _manager;

    public GetLocationFunction(ILogger<GetLocationFunction> logger,
        IGetLocationManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetLocations")]
    public async Task<IActionResult> GetLocations([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "location/getlocations")] HttpRequest req)
    {
        _logger.LogInformation("GetLocations Invoke().");
        try
        {
            var responce = await _manager.ExecuteAsync();
            return new OkObjectResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the Location the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}