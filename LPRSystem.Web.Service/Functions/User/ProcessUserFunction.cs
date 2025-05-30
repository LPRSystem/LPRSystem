using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using LPRSystem.Web.API.Manager.Services.User;
using LPRSystem.Web.API.Manager.Services;

namespace LPRSystem.Web.Service.Functions.User;

public class ProcessUserFunction
{
    private readonly ILogger<ProcessUserFunction> _logger;
    private readonly IProgressUserDataManager _manager;
    private readonly IRequestParser<LPRSystem.Web.API.Manager.Models.User.User> _requestParser;

    public ProcessUserFunction(ILogger<ProcessUserFunction> logger,
        IProgressUserDataManager manager,
        IRequestParser<LPRSystem.Web.API.Manager.Models.User.User> requestParser)
    {
        _logger = logger;
        _manager = manager;
        _requestParser = requestParser;
    }

    [Function("ProcessUserFunction")]
    public async Task<IActionResult> ProcessUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/processuser")] HttpRequest req)
    {
        _logger.LogInformation("ProcessUserFunction Invoke().");
        try
        {
            var requestModel = await _requestParser.ParseAsync(req);

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