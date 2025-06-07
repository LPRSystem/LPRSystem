using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Models.PaymentMethod;
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
        public IActionResult GetPaymentMethodById([HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "paymentmethod/getpaymentmethodbyid/{paymentmethodid}")] HttpRequest req, long paymentmethodid)
        {
            _logger.LogInformation("GetPaymentMethodById Function Invoked()");

            try
            {
                if (paymentmethodid == 0)
                    return new BadRequestObjectResult("Please send valid payment id");

                string connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);

                LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod paymentMethod = new LPRSystem.Web.API.Manager.Models.PaymentMethod.PaymentMethod();

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("[api].[uspGetPaymentMethodById]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@paymentMethodId", paymentmethodid);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
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
                }
                connection.Close();

                return new OkObjectResult(paymentMethod);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
