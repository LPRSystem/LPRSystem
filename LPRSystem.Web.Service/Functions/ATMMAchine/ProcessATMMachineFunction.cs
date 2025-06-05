using LPRSystem.Web.API.Manager.Services.ATMMachine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LPRSystem.Web.Service.Functions.ATMMAchine;

public class ProcessATMMachineFunction
{
    private readonly ILogger<ProcessATMMachineFunction> _logger;
    private readonly IProcessATMMachineManager _manager;

    public ProcessATMMachineFunction(ILogger<ProcessATMMachineFunction> logger, IProcessATMMachineManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("ProcessATMMachineFunction")]
    public async Task<IActionResult> ProcessATMMachine([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "atmmachine/processatmmachine")] HttpRequest req)
    {
        _logger.LogInformation("ProcessATMMachineFunction Invoke().");
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ATMMachine.ATMMachine>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the atm machine the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}