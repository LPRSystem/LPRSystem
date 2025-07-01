using LPRSystem.Web.API.Manager.Models.PaymentMethod;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class DeleteParkingPriceFunction
{
    private readonly ILogger<DeleteParkingPriceFunction> _logger;

    public DeleteParkingPriceFunction(ILogger<DeleteParkingPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteParkingPriceFunction")]
    public IActionResult DeleteParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route ="parkingprice/deleteparkingprice/{parkingPriceId}")] HttpRequest req, long parkingPriceId)
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[api].[uspDeleteParkingPrice]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@parkingPriceId", parkingPriceId);

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