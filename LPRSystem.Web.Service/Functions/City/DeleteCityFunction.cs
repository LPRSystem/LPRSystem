using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.City;

public class DeleteCityFunction
{
    private readonly ILogger<DeleteCityFunction> _logger;

    public DeleteCityFunction(ILogger<DeleteCityFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteCityFunction")]
    public IActionResult DeleteCity([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "city/deletelocation/{cityid}")] HttpRequest req, long cityid)
    {
        _logger.LogInformation("Delete City method invoked.");

        bool delete = false;


        return new OkObjectResult("Welcome to Azure Functions!");
    }
}