using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.PaymentMethod;

public class GetPaymentMethodsFunction
{
    private readonly ILogger<GetPaymentMethodsFunction> _logger;

    public GetPaymentMethodsFunction(ILogger<GetPaymentMethodsFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetPaymentMethodsFunction")]
    public IActionResult GetPaymentMethods([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "paymentmethod/getpaymentmethods")] HttpRequest req)
    {
        _logger.LogInformation("GetPaymentMethods Function Invoked.");
        
        List<LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod> paymentMethods = new List<API.Manager.Models.PaymentMethod.PaymentMethod>();

        LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod paymentMethod = null;

        string connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";

        SqlConnection connection = new SqlConnection(connectionString);

        connection.Open();

        SqlCommand command = new SqlCommand("[api].[uspGetPaymentMethods]", connection);

        command.CommandType = CommandType.StoredProcedure;

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            paymentMethod = new API.Manager.Models.PaymentMethod.PaymentMethod();


            paymentMethod.Id = reader.GetInt64(reader.GetOrdinal("Id"));

            paymentMethod.Name = reader.SafeGetString(reader.GetOrdinal("Name"));

            paymentMethod.Code = reader.SafeGetString(reader.GetOrdinal("Code"));

            paymentMethod.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

            if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                paymentMethod.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));

            paymentMethod.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

            if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                paymentMethod.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

            object isActiveValue = reader["IsActive"];

            paymentMethod.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

            paymentMethods.Add(paymentMethod);
        }

        connection.Close();

        return new OkObjectResult(paymentMethods);
    }
}