using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;


namespace LPRSystem.Web.Service.Functions.ParkingTicketPayment
{
    public class SaveParkingTicketPaymentFunction
    {
        private readonly ILogger<SaveParkingTicketPaymentFunction> _logger;
        public SaveParkingTicketPaymentFunction(ILogger<SaveParkingTicketPaymentFunction> logger)
        {
            _logger = logger;
        }
        [Function("SaveParkingTicketPaymentFunction")]
        public async Task<IActionResult> SaveParkingTicketPayment([HttpTrigger(AuthorizationLevel.Anonymous,"post",Route = "ParkingTicketPayment/SaveParkingTicketPayment")]HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                //Deserialize the json into your request object

                var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.
                    ParkingTicketPayment>(requestBody);

                SqlConnection sqlConnection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

               sqlConnection.Open();

                SqlCommand command = new SqlCommand("[api].[uspSaveParkingTicketPayment]",sqlConnection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@ParkingTicketId", requestModel.ParkingTicketId);

                command.Parameters.AddWithValue("@ATMId", requestModel.ATMId);

                command.Parameters.AddWithValue("@PaymentMethodId", requestModel.PaymentMethodId);

                command.Parameters.AddWithValue("@PaymentReference", requestModel.PaymentReference);

                command.Parameters.AddWithValue("@TotalAmount", requestModel.TotalAmount);

                command.Parameters.AddWithValue("@PaidAmount", requestModel.PaidAmount);

                command.Parameters.AddWithValue("@DueAmount", requestModel.DueAmount);

                command.Parameters.AddWithValue("@Status", requestModel.Status);

                command.Parameters.AddWithValue("@CreatedBy", requestModel.CreatedBy);

                command.Parameters.AddWithValue("@CreatedOn", requestModel.CreatedOn);

                command.Parameters.AddWithValue("@ModifiedBy", requestModel.ModifiedBy);

                command.Parameters.AddWithValue("@ModifiedOn", requestModel.ModifiedOn);

                command.Parameters.AddWithValue("@IsActive", requestModel.IsActive);

                int response = command.ExecuteNonQuery();

                sqlConnection.Close();

                return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
