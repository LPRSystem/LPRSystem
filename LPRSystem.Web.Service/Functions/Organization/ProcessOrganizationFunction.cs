using LPRSystem.Web.API.Manager.Models.Organization;
using LPRSystem.Web.API.Manager.Services.Organization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LPRSystem.Web.Service.Functions.Organization;

public class ProcessOrganizationFunction
{
    private readonly ILogger<ProcessOrganizationFunction> _logger;
    private readonly IProcessOrganizationManager _manager;

    public ProcessOrganizationFunction(ILogger<ProcessOrganizationFunction> logger,
        IProcessOrganizationManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("ProcessOrganizationFunction")]
    public async Task<IActionResult> ProcessOrganization([HttpTrigger(AuthorizationLevel.Anonymous, "post",
        Route = "organizations/processorganization")] HttpRequest req)
    {
        _logger.LogInformation("ProcessOrganizationFunction Invoke().");
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Organization.Organization>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the organizations the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}