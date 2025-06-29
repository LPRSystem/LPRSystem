using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class UpdateParkingPriceFunction
{
    private readonly ILogger<UpdateParkingPriceFunction> _logger;

    public UpdateParkingPriceFunction(ILogger<UpdateParkingPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("UpdateParkingPriceFunction")]
    public async Task< IActionResult> UpdateParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route ="parkingprice/updateparkingprice")] HttpRequest req)
    {
        _logger.LogInformation($"Update Parking Price Function Invoked()");

        // Read the request body
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var parkingPriceToUpdate = JsonConvert.DeserializeObject<API.Manager.Models.ParkingPrice.ParkingPrice>(requestBody);

        if (parkingPriceToUpdate == null)
        {
            return new BadRequestObjectResult("Invalid parking price data.");
        }

        string connectionString = Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting);

        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("[api].[uspUpdateParkingPrice]", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Add parameters
            command.Parameters.AddWithValue("@parkingPriceId", parkingPriceToUpdate.ParkingPriceId);
            command.Parameters.AddWithValue("@duration", parkingPriceToUpdate.Duration);
            command.Parameters.AddWithValue("@price", parkingPriceToUpdate.Price);
            command.Parameters.AddWithValue("@modifiedBy", parkingPriceToUpdate.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedOn", parkingPriceToUpdate.ModifiedOn);
            command.Parameters.AddWithValue("@isActive", parkingPriceToUpdate.IsActive);
            command.ExecuteNonQuery();
            connection.Close();

            return new OkObjectResult(parkingPriceToUpdate);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}