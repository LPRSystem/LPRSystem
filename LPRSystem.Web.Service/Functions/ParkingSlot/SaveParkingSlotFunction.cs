using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager;

namespace LPRSystem.Web.Service.Functions.ParkingSlot;

public class SaveParkingSlotFunction
{
    private readonly ILogger<SaveParkingSlotFunction> _logger;

    public SaveParkingSlotFunction(ILogger<SaveParkingSlotFunction> logger)
    {
        _logger = logger;
    }

    [Function("SaveParkingSlotFunction")]
    public async Task<IActionResult> SaveParkingSlot([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "parkingslot/saveparkingslot")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingSlot.ParkingSlot>(requestBody);

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspInsertParkingSlot]", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@parkingPlaceId", requestModel.ParkingPlaceId);
            command.Parameters.AddWithValue("@parkingSlotCode", requestModel.ParkingSlotCode);
            command.Parameters.AddWithValue("@atmId", requestModel.ATMId);
            command.Parameters.AddWithValue("@createdBy", requestModel.CreatedBy);
            command.Parameters.AddWithValue("@createdOn", requestModel.CreatedOn);
            command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn);
            command.Parameters.AddWithValue("@isActive", requestModel.IsActive);
                       
            int response = command.ExecuteNonQuery();

            connection.Close();

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}