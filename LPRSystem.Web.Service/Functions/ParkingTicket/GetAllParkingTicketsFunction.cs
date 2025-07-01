using LPRSystem.Web.API.Manager.Services.ParkingTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.ParkingTicket;

public class GetAllParkingTicketsFunction
{
    private readonly ILogger<GetAllParkingTicketsFunction> _logger;
    private readonly IGetParkingTicketsManager _manager;
    public GetAllParkingTicketsFunction(ILogger<GetAllParkingTicketsFunction> logger, IGetParkingTicketsManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [Function("GetAllParkingTicketsFunction")]
    public async Task< IActionResult> GetAllParkingTickets([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route ="parkingticket/getallparkingtickets")] HttpRequest req)
    {
        _logger.LogInformation("GetAllParkingTickets Invoked..");
        try 
        {
            var response = await _manager.ExecuteAsync();
            return new OkObjectResult(response);
        }
        catch(Exception ex)  
        {
            _logger.LogError(ex, "An error occurred while fetching the Parking Tickets request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}