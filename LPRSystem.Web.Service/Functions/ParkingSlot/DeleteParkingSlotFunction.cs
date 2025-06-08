using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.ParkingSlot;

namespace LPRSystem.Web.Service.Functions.ParkingSlot;

public class DeleteParkingSlotFunction
{
    private readonly ILogger<DeleteParkingSlotFunction> _logger;
    private bool isDeleted;
    private object parkingSlotId;

    public DeleteParkingSlotFunction(ILogger<DeleteParkingSlotFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteParkingSlotFunction")]
    public async Task<IActionResult> DeleteParkingSlot([HttpTrigger(AuthorizationLevel.Anonymous, "delete", "parkingslot/deleteparkingslot/{parkingSlotId}")] HttpRequest req, long parkingSlotId)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[api].[uspDeleteParkingSlot]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@parkingSlotId", parkingSlotId);

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    isDeleted = result == 1 ? true : false;

                    return new OkObjectResult(isDeleted);
                }
            }
        }
        catch (Exception ex)
        {
           throw ex;
        }

    }
}

    
