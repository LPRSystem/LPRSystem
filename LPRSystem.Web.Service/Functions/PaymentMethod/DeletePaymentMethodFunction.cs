using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.PaymentMethod
{
    public class DeletePaymentMethodFunction
    {
        private readonly ILogger<DeletePaymentMethodFunction> _logger;

        public DeletePaymentMethodFunction(ILogger<DeletePaymentMethodFunction> logger)
        {
            _logger = logger;
        }

        [Function("DeletePaymentMethod")]
        public async Task<IActionResult> DeletePaymentMethod([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
            Route = "paymentmethod/deletepaymentmethod/{paymentmethodid}")] HttpRequest req,
            long paymentmethodid)
        {
            _logger.LogInformation("Delete payment method invoked.");

            bool deleted = false;

            SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspDeletePaymentMethod]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", paymentmethodid);

            var response = sqlCommand.ExecuteNonQuery();

            connection.Close();

            deleted = response == 1 ? true : false;

            return new OkObjectResult(deleted);
        }
    }
}
