using LPRSystem.Web.API.Manager.Services.ATMMachine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.ATMMAchine;

public class GetATMMachineFunction
{
    private readonly ILogger<GetATMMachineFunction> _logger;
    private readonly IGetATMMachineManager _manager;

    public GetATMMachineFunction(ILogger<GetATMMachineFunction> logger, IGetATMMachineManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetATMMachines")]
    public async Task< IActionResult> GetATMMachines([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "atmmachine/getatmmachines")] HttpRequest req)
    {
        _logger.LogInformation("GetATMMachines Invoke().");
        try
        {
            var responce = await _manager.ExecuteAsync();
            return new OkObjectResult(responce);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching the ATM Machine the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}