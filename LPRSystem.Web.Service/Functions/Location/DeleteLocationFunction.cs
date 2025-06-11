using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.Location;

public class DeleteLocationFunction
{
    private readonly ILogger<DeleteLocationFunction> _logger;

    public DeleteLocationFunction(ILogger<DeleteLocationFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteLocationFunction")]
    public async Task< IActionResult> DeleteLocation([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route ="location/deletelocation/{locationid}")] HttpRequest req, long locationid)
    {
        _logger.LogInformation("Delete Location method invoked.");

        bool delete = false;


        return new OkObjectResult("Welcome to Azure Functions!");
    }
}