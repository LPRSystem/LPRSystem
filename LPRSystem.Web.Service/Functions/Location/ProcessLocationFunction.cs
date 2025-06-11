using LPRSystem.Web.API.Manager.Services.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LPRSystem.Web.Service.Functions.Location;

public class ProcessLocationFunction
{
    private readonly ILogger<ProcessLocationFunction> _logger;
    private readonly IProcessLocationManager _manager;

    public ProcessLocationFunction(ILogger<ProcessLocationFunction> logger, IProcessLocationManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("ProcessLocationFunction")]
    public async Task< IActionResult> ProcessLocation([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "location/processlocation")] HttpRequest req)
    {
        _logger.LogInformation("ProcessLocationFunction Invoke().");
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Location.Location>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the location the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}