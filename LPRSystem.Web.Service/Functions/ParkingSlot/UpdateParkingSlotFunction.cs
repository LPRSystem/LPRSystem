using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager;

namespace LPRSystem.Web.Service.Functions.ParkingSlot
{
    public class UpdateParkingSlotFunction
    {
        private readonly ILogger<UpdateParkingSlotFunction> _logger;

        public UpdateParkingSlotFunction(ILogger<UpdateParkingSlotFunction> logger)
        {
            _logger = logger;
        }

        [Function("UpdateParkingSlotFunction")]
        public async Task<IActionResult> UpdateParkingSlot(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put",
            Route = "parkingslot/updateparkingslot/{parkingSlotId}")] HttpRequest req,
            long parkingSlotId)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingSlot.ParkingSlot>(requestBody);

                using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
                {
                    connection.Open();

                    if (connection == null) throw new Exception("SqlConnection is null");

                    using (SqlCommand command = new SqlCommand("[api].[uspUpdateParkingSlot]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@parkingSlotId", requestModel.ParkingSlotId);
                        command.Parameters.AddWithValue("@parkingPlaceId", requestModel.ParkingPlaceId);
                        command.Parameters.AddWithValue("@parkingSlotCode", requestModel.ParkingSlotCode);
                        command.Parameters.AddWithValue("@atmId", requestModel.ATMId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
                        command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@isActive", requestModel.IsActive);

                        int result = command.ExecuteNonQuery();
                        connection.Close();

                        return result > 0
                            ? new OkObjectResult("ParkingSlot updated successfully.")
                            : new NotFoundObjectResult("ParkingSlot not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

