using LPRSystem.Web.API.Manager.Services;
namespace LPRSystem.Web.API.Functions.Vehicles
{
    public class GetVehiclesByIdFunction
    {
        private readonly ILogger<GetVehicleByIdFunction> logger;
        private readonly IGetVehiclesByIdManager _manager;
        private readonly IRequestParser<GetVehicleByIdRequest> requestParser;

        public GetVehiclesByIdFunction(ILogger<GetVehicleByIdFunction> _logger, IGetVehiclesByIdManager manager, IRequestParser<GetVehiclesByIdRequest> _requestParser)
        {
            _logger = logger;
            _manager = manager;
            _requestParser = requestParser;
        }
    }
}
