using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.Service.Functions.PaymentMethod
{
    public class GetPaymentMethodByIdFunction
    {
        private readonly ILogger<GetPaymentMethodByIdFunction> _logger;

        public GetPaymentMethodByIdFunction(ILogger<GetPaymentMethodByIdFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetPaymentMethodByIdFunction")]
        public async Task<IActionResult> GetPaymentMethodById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/getuserbyid")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function proceed a request.");

            <PaymentMethod> paymentMethod= new <PaymentMethod>();
            PaymentMethod result= null;

            var deletePaymentMethodId = req.Query["PaymentMethodId"].ToString();

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable
                (Global.CommonSQLServerConnectionStringSetting));
            connection.Open();
            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetByIdPaymentMethod]",connection);
            sqlCommand.CommandType =CommandType.StoredProcedure;
            sqlCommand.Parameters.AddWithValue("@id",deletePaymentMethodId);
            sqlCommand.ExecuteScalarAsync();
            connection.Close();
            return new OkObjectResult(paymentMethod);
        }
    }
}
