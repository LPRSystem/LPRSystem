using LPRSystem.Web.API.Manager.Models.City;
using LPRSystem.Web.API.Manager.Services;
using LPRSystem.Web.API.Manager.Services.City;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.City;

public class GetCityByIdFunction
{
    private readonly ILogger<GetCityByIdFunction> _logger;
    private readonly IGetCityByIdManager _manager;
    private readonly IRequestParser<GetCityByIdRequest> _requestParser;
    public GetCityByIdFunction(ILogger<GetCityByIdFunction> logger, IGetCityByIdManager manager, IRequestParser<GetCityByIdRequest> requestParser)
    {
        _logger = logger;
        _manager = manager;
        _requestParser = requestParser;
    }

    [Function("GetCityByIdFunction")]
    public async Task< IActionResult> GetCirtById([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route ="city/getcitybyid")] HttpRequest req)
    {
        _logger.LogInformation("Get City By Id request.");
        try
        {
            var request = await _requestParser.ParseAsync(req);


            if (request.CityId <= 0)
            {
                _logger.LogWarning("Invalid CityId provided: {CityId}", request.CityId);
                return new BadRequestObjectResult("Invalid CityId parameter.");
            }
            var response = await _manager.ExecuteAsync(request);

            if (response == null)
            {
                _logger.LogWarning("No user found for cityid: {CityId}", request.CityId);
                return new NotFoundObjectResult($"No user found for cityid:  {request.CityId}");

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