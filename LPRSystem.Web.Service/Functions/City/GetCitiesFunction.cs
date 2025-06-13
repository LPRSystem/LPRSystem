using LPRSystem.Web.API.Manager.Services.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.City;

public class GetCitiesFunction
{
    private readonly ILogger<GetCitiesFunction> _logger;
    private readonly IGetCitiesManager _manager;
    public GetCitiesFunction(ILogger<GetCitiesFunction> logger,
        IGetCitiesManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetCitiesFunction")]
    public async Task<IActionResult> GetCities([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "city/getcities")] HttpRequest req)
    {
        _logger.LogInformation("GetCitiesFunction Invoked().");
        try
        {
            var response = await _manager.ExecuteAsync();
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the City request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        }
    }
}