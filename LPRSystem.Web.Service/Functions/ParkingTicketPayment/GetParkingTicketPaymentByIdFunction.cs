using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingTicketPayment
{
    public class GetParkingTicketPaymentByIdFunction
    {
        private readonly ILogger<GetParkingTicketPaymentByIdFunction> _logger;
        public GetParkingTicketPaymentByIdFunction(ILogger<GetParkingTicketPaymentByIdFunction> logger)
        {
            _logger = logger;
        }
        [Function("GetParkingTicketPaymentByIdFunction")]
        public IActionResult GetParkingTicketPaymentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ParkingTicketPayment/GetParkingTicketPaymentById/{parkingTicketPaymentId}")] HttpRequest req, long parkingTicketPaymentId)
        {
            _logger.LogInformation("GetParkinTicketPaymentByById Function Invoked().");

            try
            {
                if (parkingTicketPaymentId == 0)
                    return new BadRequestObjectResult("please send valid parkingTicketPaymentId");

                string connctionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";

                LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment parkingTicketPayment = new LPRSystem.Web.API.Manager.Models.ParkingTicketPayment.ParkingTicketPayment();

                SqlConnection connection = new SqlConnection(connctionString);
                connection.Open();
                SqlCommand command = new SqlCommand("[api].[uspGetParkingTicketPaymentId]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@parkingTicketPaymentId", parkingTicketPaymentId);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
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
