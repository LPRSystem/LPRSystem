using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

//namespace LPRSystem.Web.Service.Functions.PaymentMethod
//{
    //public class UpdatePaymentMethodFunction
    //{
    //    private readonly ILogger<UpdatePaymentMethodFunction> _logger;

    //    public UpdatePaymentMethodFunction(ILogger<UpdatePaymentMethodFunction> logger)
    //    {
    //        _logger = logger;
    //    }

//        [Function("UpdatePaymentMethod")]
//        //public async Task<IActionResult> Run(
//        //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req)
//        {
//            _logger.LogInformation("UpdatePaymentMethod function Invoked.");

//            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
//            var data = JsonConvert.DeserializeObject<UpdatePaymentMethodFunction>(requestBody);


//            if (data == null)
//            {
//                return new BadRequestObjectResult("Invalid or missing PaymentMethod data.");
//            }

//            try
//            {
//                using (SqlConnection connection = new SqlConnection(
//                    Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
//                {
//                    await connection.OpenAsync();

//                    using (SqlCommand command = new SqlCommand("[api].[uspUpdatePaymentMethod]", connection))
//                    {
//                        command.CommandType = CommandType.StoredProcedure;

//                        //command.Parameters.AddWithValue("@Id", data.Id);
//                        //command.Parameters.AddWithValue("@Name", data.Name ?? (object)DBNull.Value);
//                        //command.Parameters.AddWithValue("@Code", data.Code ?? (object)DBNull.Value);
//                        //command.Parameters.AddWithValue("@ModifiedBy", data.ModifiedBy);

//                        await command.ExecuteNonQueryAsync();

//                        //return new OkObjectResult($"Payment method with ID {data.Id} updated successfully.");
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error occurred while updating payment method.");
//                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
//            }
//        }
//    }

//}
