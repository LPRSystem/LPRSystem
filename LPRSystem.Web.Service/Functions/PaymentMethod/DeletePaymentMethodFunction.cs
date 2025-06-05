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
using LPRSystem.Web.API.Manager.Models.PaymentMethod;

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
        public async Task<IActionResult> DeletePaymentMethod([HttpTrigger(AuthorizationLevel.Anonymous, "Delete", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("Delete payment method invoked.");

            LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod paymentMethod = new LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod();

            var deletePaymentMethodId = req.Query["paymentMethod"].ToString();

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            connection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspPaymentMethod]", connection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@id", deletePaymentMethodId);

            sqlCommand.ExecuteNonQuery();

            connection.Close();

            return new OkObjectResult(paymentMethod);
        }
    }
}
