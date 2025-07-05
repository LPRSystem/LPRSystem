using LPRSystem.Web.API.Manager.Models.ParkingTicket;
using LPRSystem.Web.API.Manager.Services;
using LPRSystem.Web.API.Manager.Services.ParkingTicket;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace LPRSystem.Web.Service.Functions.ParkingTicket;

public class GetAllParkingTicketByATMIdParkedOn
{
    private readonly ILogger<GetAllParkingTicketByATMIdParkedOn> _logger;
    private readonly IGetAllParkingTicketByATMIdParkedOnManager _manager;
    private readonly IRequestParser<GetAllParkingTicketByATMIdParkedOnRequest> _requestParser;


    public GetAllParkingTicketByATMIdParkedOn(ILogger<GetAllParkingTicketByATMIdParkedOn> logger
        , IGetAllParkingTicketByATMIdParkedOnManager manager,
        IRequestParser<GetAllParkingTicketByATMIdParkedOnRequest> requestParser)
    {
        _logger = logger;
        _manager = manager;
        _requestParser = requestParser;
    }

    [Function("GetAllParkingTicketByATMIdParkedOn")]
    public async Task< IActionResult> GetAllParkingTicketByATMIdAndParkedOn([HttpTrigger(AuthorizationLevel.Anonymous, "get",Route = "parkingticket/getallparkingticketbyatmidparkedon")] HttpRequest req)
    {
        _logger.LogInformation(" get Parking ticket by ATMId and Parkedon request");
        try
        {
            var request = await _requestParser.ParseAsync(req);

            if (request.ATMId <= 0)
            {
                _logger.LogWarning("Invalid ATMId provided: {ATMId}", request.ATMId);
                return new BadRequestObjectResult("Invalid ATMId parameter.");
            }
           

            var response = await _manager.ExecuteAsync(request);
           
            if (response == null)
            {
                _logger.LogWarning("No user found for ATMId: {ATMId}", request.ATMId);
                return new NotFoundObjectResult($"No user found for ATMId:  {request.ATMId}");

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