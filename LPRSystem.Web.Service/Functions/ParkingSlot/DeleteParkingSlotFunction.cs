using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
namespace LPRSystem.Web.Service.Functions.ParkingSlot;

public class DeleteParkingSlotFunction
{
    private readonly ILogger<DeleteParkingSlotFunction> _logger;
    public DeleteParkingSlotFunction(ILogger<DeleteParkingSlotFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteParkingSlotFunction")]
    public IActionResult DeleteParkingSlot([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
        Route = "parkingslot/deleteparkingslot/{parkingSlotId}")] HttpRequest req, long parkingSlotId)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[api].[uspDeleteParkingSlot]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@parkingSlotId", parkingSlotId);

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    return new OkObjectResult(result == 1 ? true : false);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}


