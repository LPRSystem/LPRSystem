using LPRSystem.Web.API.Functions.Vehicles;
using LPRSystem.Web.API.Manager.Services.Vehicles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace LPRSystem.Web.API.Functions.Vehicles
{
    public class GetVehiclesFunction
    {
        private readonly ILogger<GetVehiclesFunction> _logger;
        private readonly IGetVehiclesManager _manager;
        public GetVehiclesFunction(ILogger<GetVehiclesFunction> logger, IGetVehiclesManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Function("GetVehicles")]
        public async Task<IActionResult> GetVehicles([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req, ILogger log);
        
    }
}
