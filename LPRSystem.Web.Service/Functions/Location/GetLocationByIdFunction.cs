using LPRSystem.Web.API.Manager.Models.Location;
using LPRSystem.Web.API.Manager.Services;
using LPRSystem.Web.API.Manager.Services.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.Location;

public class GetLocationByIdFunction
{
    private readonly ILogger<GetLocationByIdFunction> _logger;
    private readonly IGetLocationByIdManager _manager;
    private readonly IRequestParser<GetLocationByIdRequest> _requestParser;

    public GetLocationByIdFunction(ILogger<GetLocationByIdFunction> logger, IGetLocationByIdManager manager,
        IRequestParser<GetLocationByIdRequest> requestParser)
    {
        _logger = logger;
        _manager = manager;
        _requestParser = requestParser;
    }

    [Function("GetLocationByIdFunction")]
    public async Task< IActionResult> GetLocationById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "location/getlocationbyid/{locationid}")] HttpRequest req, long locationid)
    {
        _logger.LogInformation("Get Location By Id request.");
        try
        {
            var request = await _requestParser.ParseAsync(req);


            if (request.LocationId <= 0)
            {
                _logger.LogWarning("Invalid LocationId provided: {LocationId}",request.LocationId);
                return new BadRequestObjectResult("Invalid LocationId parameter.");
            }
            var response = await _manager.ExecuteAsync(request);

            if (response == null)
            {
                _logger.LogWarning("No user found for locationId: {LocationId}", request.LocationId);
                return new NotFoundObjectResult($"No user found for locationId:  { request.LocationId }");

            }
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}