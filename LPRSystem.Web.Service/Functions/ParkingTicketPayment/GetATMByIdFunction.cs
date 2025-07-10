using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Models;
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

namespace LPRSystem.Web.Service.Functions.ParkingTicketPayment
{
    public class GetATMByIdFunction
    {
        private readonly ILogger<GetATMByIdFunction> _logger;

        public GetATMByIdFunction(ILogger<GetATMByIdFunction> logger)
        {
            _logger = logger;
        }
        [Function("GetATMByIdFunction")]
        public IActionResult GetATMById([HttpTrigger(AuthorizationLevel.Anonymous,"get",Route ="ParkingTicketPayment/GetATMById/{ATMId}")]HttpRequest req,long ATMId)
        {
            _logger.LogInformation("GetATMId Function invoked().");
            try
            {
                if (ATMId == 0)
                    return new BadRequestObjectResult("Please send valid payment id");

                string connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";

                LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment parkingTicketPayment = new LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment();

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("[api].[uspGetATMById]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ATMId", ATMId);
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    parkingTicketPayment.ParkingTicketPaymentId = reader.GetInt64(reader.GetOrdinal("ParkingTicketPaymentId"));

                    parkingTicketPayment.ParkingTicketId = reader.GetInt64(reader.GetOrdinal("ParkingTicketId"));

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

                  
                }
                connection.Close();

                return new OkObjectResult(parkingTicketPayment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
