using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Functions.Vehicles
{
    public class DeleteVehiclesFunction
    {
        private readonly ILogger<DeleteVehiclesFunction> _logger;

        public DeleteVehiclesFunction(ILogger<DeleteVehiclesFunction> logger)
        {
            _logger = logger;
        }
        [Function("DeleteVehicles")]
        public async Task<IActionResult> DeleteVehicles([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# Http trigger function processed a request");
            
        }
    }
}
