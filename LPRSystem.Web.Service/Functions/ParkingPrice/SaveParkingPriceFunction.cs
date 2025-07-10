using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
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
    public async Task< IActionResult> SaveParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route ="parkingprice/saveparkingprice")] HttpRequest req)
    {
        try
        {
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            //Deserialize the json into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice>(requestBody);

            SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");
            connection.Open();
            SqlCommand command = new SqlCommand("[api].[uspInsertParkingPrice]", connection);

            command.CommandType = CommandType.StoredProcedure;

            
            command.Parameters.AddWithValue("@duration", requestModel.Duration);
            command.Parameters.AddWithValue("@price", requestModel.Price);
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

