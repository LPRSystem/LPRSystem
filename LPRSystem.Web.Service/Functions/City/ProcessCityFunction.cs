using LPRSystem.Web.API.Manager.Services.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LPRSystem.Web.Service.Functions.City;

public class ProcessCityFunction
{
    private readonly ILogger<ProcessCityFunction> _logger;
    private readonly IProcessCityManager _manager;

    public ProcessCityFunction(ILogger<ProcessCityFunction> logger, IProcessCityManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("ProcessCityFunction")]
    public async Task< IActionResult> ProcessCity([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route ="city/processcity")] HttpRequest req)
    {
        _logger.LogInformation("ProcessCityFunction Invoke().");
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.City.City>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the City the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}