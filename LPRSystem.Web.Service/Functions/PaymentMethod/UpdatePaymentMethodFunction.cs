using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Formatters;
using Google.Protobuf.WellKnownTypes;

namespace LPRSystem.Web.Service.Functions.PaymentMethod
{
    public class UpdatePaymentMethodFunction
    {
        private readonly ILogger<UpdatePaymentMethodFunction> _logger;

        public UpdatePaymentMethodFunction(ILogger<UpdatePaymentMethodFunction> logger)
        {
            _logger = logger;
        }

        [Function("UpdatePaymentMethodFunction")]
        public async Task<IActionResult> UpdatePaymentMethod(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = "paymentmethod/updatepaymentmethod")] HttpRequest req)
        {
            _logger.LogInformation($"UpdatePaymentMethod Function Invoked()");

            // Read the request body
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var paymentMethodToUpdate = JsonConvert.DeserializeObject<API.Manager.Models.PaymentMethod.PaymentMethod>(requestBody);

            if (paymentMethodToUpdate == null)
            {
                return new BadRequestObjectResult("Invalid payment method data.");
            }

            string connectionString = Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting);


            //please write your own ,dont copy from anywhere


            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("[api].[uspUpdatePaymentMethod]", connection);
                command.CommandType = CommandType.StoredProcedure;

                // Add parameters
                command.Parameters.AddWithValue("@id", paymentMethodToUpdate.Id);
                command.Parameters.AddWithValue("@name", paymentMethodToUpdate.Name);
                command.Parameters.AddWithValue("@code", paymentMethodToUpdate.Code);
                command.Parameters.AddWithValue("@modifiedBy", paymentMethodToUpdate.ModifiedBy);
                command.Parameters.AddWithValue("@modifiedOn", paymentMethodToUpdate.ModifiedOn);
                command.Parameters.AddWithValue("@isActive", paymentMethodToUpdate.IsActive);
                command.ExecuteNonQuery();
                connection.Close();

                return new OkObjectResult(paymentMethodToUpdate);
            }
            catch (Exception ex) {
                throw ex;
            }

        }
    }
}
