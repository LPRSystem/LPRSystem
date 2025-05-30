using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.PaymentMethod;

public class SavePaymentMethodFunction
{
    private readonly ILogger<SavePaymentMethodFunction> _logger;

    public SavePaymentMethodFunction(ILogger<SavePaymentMethodFunction> logger)
    {
        _logger = logger;
    }

    [Function("SavePaymentMethodFunction")]
    public async Task<IActionResult> SavePaymentMethod([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "paymentmethod/savepaymentmethod")] HttpRequest req)
    {
        _logger.LogInformation("SavePaymentMethod Function Invoked");

        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod>(requestBody);

            string connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspSavePaymentMethod]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@code", requestModel.Code);
            command.Parameters.AddWithValue("@createdby", requestModel.CreatedBy);
            command.Parameters.AddWithValue("@createdon", requestModel.CreatedOn);
            command.Parameters.AddWithValue("@modifiedby", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedon", requestModel.ModifiedOn);
            command.Parameters.AddWithValue("@isactive", requestModel.IsActive);

            command.ExecuteNonQuery();

            connection.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return new OkObjectResult("Welcome to Azure Functions!");
    }
}