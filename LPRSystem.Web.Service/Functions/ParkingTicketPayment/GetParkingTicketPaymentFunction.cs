
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager;
using Microsoft.Azure.Functions.Worker;

namespace LPRSystem.Web.Service.Functions.ParkingTicketPayment
{
    public class GetParkingTicketPaymentFunction
    {
        private readonly ILogger<GetParkingTicketPaymentFunction> _logger;
        public GetParkingTicketPaymentFunction(ILogger<GetParkingTicketPaymentFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetParkingTicketPayment")]
        public IActionResult GetParkingTicketPayment([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ParkingTicketPayment/GetParkingTicketPayment")] HttpRequest req)
        {
            try
            {
                List<LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment> parkingTicketPayments = new List<LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment>();

                LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment parkingTicketPayment = null;

                SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

                connection.Open();

                SqlCommand command = new SqlCommand("[api].[uspGetAllParkingTicketPayment]", connection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    parkingTicketPayment = new API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment();

                    parkingTicketPayment.ParkingTicketPaymentId = reader.GetInt64(reader.GetOrdinal("ParkingTicketPaymentId"));

                    parkingTicketPayment.ATMId = reader.GetInt64(reader.GetOrdinal("ATMId"));

                    parkingTicketPayment.PaymentMethodId = reader.GetInt64(reader.GetOrdinal("PaymentMethodId"));

                    parkingTicketPayment.PaymentReference = reader.SafeGetString(reader.GetOrdinal("PaymentReference"));

                    parkingTicketPayment.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));

                    parkingTicketPayment.PaidAmount = reader.GetDecimal(reader.GetOrdinal("PaidAmount"));

                    parkingTicketPayment.DueAmount = reader.GetDecimal(reader.GetOrdinal("DueAmount"));

                    parkingTicketPayment.Status = reader.SafeGetString(reader.GetOrdinal("Status"));

                    parkingTicketPayment.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                    if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                        parkingTicketPayment.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));

                    parkingTicketPayment.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

                    if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                        parkingTicketPayment.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

                    object isActiveValue = reader["IsActive"];

                    parkingTicketPayment.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

                    parkingTicketPayments.Add(parkingTicketPayment);
                }

                connection.Close();

                return new OkObjectResult(parkingTicketPayments);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
    }
}
                
                
    

    

