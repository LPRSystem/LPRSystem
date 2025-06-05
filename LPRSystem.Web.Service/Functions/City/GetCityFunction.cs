using LPRSystem.Web.API.Manager.Services.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.City;

public class GetCityFunction
{
    private readonly ILogger<GetCityFunction> _logger;
    private readonly IGetCityManager _manager;

    public GetCityFunction(ILogger<GetCityFunction> logger, IGetCityManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetCityFunction")]
    public async Task< IActionResult> GetCity([HttpTrigger(AuthorizationLevel.Function, "get",Route = "city/getcity")] HttpRequest req)
    {
        _logger.LogInformation("GetCity Invoked().");
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