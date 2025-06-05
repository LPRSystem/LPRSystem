using LPRSystem.Web.API.Manager.Services.State;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.State;

public class GetStateFunction
{
    private readonly ILogger<GetStateFunction> _logger;
    private readonly IGetStateManager _manager;
    public GetStateFunction(ILogger<GetStateFunction> logger, IGetStateManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetStateFunction")]
    public async Task< IActionResult> GetState([HttpTrigger(AuthorizationLevel.Function, "get", Route = "state/getstate")] HttpRequest req)
    {
        _logger.LogInformation("GetStateFunction Invoke().");
        try
        {
            var responce = await _manager.ExecuteAsync();
            return new OkObjectResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the State the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}