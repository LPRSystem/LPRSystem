using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Models.Vehicles;
using Microsoft.Extensions.Configuration;

namespace LPRSystem.Web.API.Manager.Services.Vehicles
{
    public class VehiclesProcessRepository :BaseRepository, IVehiclesProcessRepository
    {
        public VehiclesProcessRepository(IConfiguration configuration) : base(configuration)
        { 
        }
        public async Task<IEnumerable<Models.Vehicles.Vehicles>> ExecuteAsync(VehicleProcessRequest request)
        {
            return await base.QueryAsync<Models.Vehicles.Vehicles> (CommonConstants.CommonDB, VehiclesConstants.InsertVehicles, new { vehiclesName = request.VehiclesName, vehicleType = request.VehicleType, licencePlate=request.LicencePlate, OwnerName=request.OwnerName, contactNumber=request.ContactNumber, createdBy = request.CreatedBy, createdOn = request.CreatedOn, modifiedBy = request.ModifiedBy, modifiedOn = request.ModifiedOn, isActive = request.IsActive }, null);
        }
    }
}
