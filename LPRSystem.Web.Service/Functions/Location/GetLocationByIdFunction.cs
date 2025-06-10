using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.Location;

public class GetLocationByIdFunction
{
    private readonly ILogger<GetLocationByIdFunction> _logger;

    public GetLocationByIdFunction(ILogger<GetLocationByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetLocationByIdFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}