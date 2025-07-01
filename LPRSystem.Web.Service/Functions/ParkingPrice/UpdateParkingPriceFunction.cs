using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class UpdateParkingPriceFunction
{
    private readonly ILogger<UpdateParkingPriceFunction> _logger;

    public UpdateParkingPriceFunction(ILogger<UpdateParkingPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("UpdateParkingPriceFunction")]
    public async Task<IActionResult> ParkingPriceFunction([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "parkingprice/updateparkingprice/{parkingPriceId}")] HttpRequest req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        // Deserialize the JSON into your request object
        var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice>(requestBody);
        try
        {
            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));
            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspUpdateParkingPrice]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@parkingPriceId", requestModel.ParkingPriceId);
            command.Parameters.AddWithValue("@duration", requestModel.Duration);
            command.Parameters.AddWithValue("@price", requestModel.Price);
            command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@isActive", requestModel.IsActive);

            int response = command.ExecuteNonQuery();

            connection.Close();

            return new OkObjectResult(requestModel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}