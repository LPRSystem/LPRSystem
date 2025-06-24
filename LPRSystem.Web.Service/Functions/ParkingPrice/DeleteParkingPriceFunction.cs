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
    public IActionResult DeleteParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route ="parkingprice/deleteparkingprice{parkingpriceid}")] HttpRequest req, long parkingpriceid)
    {
        _logger.LogInformation("Delete parking price invoked.");

        bool deleted = false;

        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

        connection.Open();

        SqlCommand sqlCommand = new SqlCommand("[api].[uspDeleteParkingPrice]", connection);

        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.Parameters.AddWithValue("@parkingpriceid", parkingpriceid);

        var response = sqlCommand.ExecuteNonQuery();

        connection.Close();

        deleted = response == 1 ? true : false;

        return new OkObjectResult(deleted);
    }
}