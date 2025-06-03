using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task<IActionResult> DeletePaymentMethodFunctionRun(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("DeletePaymentMethod function Invoked.");

            string paymentMethodId = req.Query["paymentMethodId"];

            if (string.IsNullOrEmpty(paymentMethodId))
            {
                return new BadRequestObjectResult("Please provide a paymentMethodId.");
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(
                    Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("[api].[uspDeletePaymentMethod]", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", paymentMethodId); // Replace with actual param name

                        var response = await command.ExecuteNonQueryAsync();

                        if (response > 0)
                        {
                            return new OkObjectResult($"Payment method with ID {paymentMethodId} deleted successfully.");
                        }
                        else
                        {
                            return new NotFoundObjectResult($"Payment method with ID {paymentMethodId} not found.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting payment method.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
