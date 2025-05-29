using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using LPRSystem.Web.API.Manager.Services.User;
using Newtonsoft.Json;

namespace LPRSystem.Web.Service.Functions.User;

public class ProcessUserFunction
{
    private readonly ILogger<ProcessUserFunction> _logger;
    private readonly IProgressUserDataManager _manager; 


    public ProcessUserFunction(ILogger<ProcessUserFunction> logger,
        IProgressUserDataManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("ProcessUserFunction")]
    public async Task<IActionResult> ProcessUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/processuser")] HttpRequest req)
    {
        _logger.LogInformation("ProcessUserFunction Invoke().");
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.User.User>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the User the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}