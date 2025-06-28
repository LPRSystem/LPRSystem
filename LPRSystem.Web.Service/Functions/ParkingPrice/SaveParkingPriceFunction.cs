using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class SaveParkingPriceFunction
{
    private readonly ILogger<SaveParkingPriceFunction> _logger;

    public SaveParkingPriceFunction(ILogger<SaveParkingPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("SaveParkingPriceFunction")]
    public async Task<IActionResult> SaveParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "parkingprice/saveparkingprice")] HttpRequest req)
    {
        _logger.LogInformation(" C# Http trigger function processed a request");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        //Deserialize the json into your request object
        var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice>(requestBody);

        string connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand("", connection);

        //sqlCommand.CammandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@duration", requestModel.Duration);
        command.Parameters.AddWithValue("@price", requestModel.Price);
        command.Parameters.AddWithValue("@createdby", requestModel.CreatedBy);
        command.Parameters.AddWithValue("@createdon", requestModel.CreatedOn);
        command.Parameters.AddWithValue("@modifiedby", requestModel.ModifiedBy);
        command.Parameters.AddWithValue("@modifiedon", requestModel.ModifiedOn);
        command.Parameters.AddWithValue("@isactive", requestModel.IsActive);
        command.ExecuteNonQuery();
        connection.Close();

        return new OkObjectResult("Welcome to Azure Functions");
    }
}