
using LPRSystem.Web.API.Manager.Models.Vehicles;
using Microsoft.AspNetCore.Http;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class GetVehiclesByIdRequestParser :IRequestParser<GetVehiclesByIdRequest>
    {
        public Task <GetVehiclesByIdRequest> ParseAsync(HttpRequest request)
        {
            var vehicleId = request.Query["VehicleId"].ToString();

            var requestModel = new GetVehiclesByIdRequest
            {
                VehicleId = !string.IsNullOrEmpty(vehicleId) ? Convert.ToInt64(vehicleId) : 0,
            };
            return Task.FromResult(requestModel);
        }
    }
}
