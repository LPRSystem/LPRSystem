using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;

namespace LPRSystem.Web.Service.Functions.ParkingTicket
{
    public class SaveParingTicketFunction
    {
        private readonly ILogger<SaveParingTicketFunction> _logger;

        public SaveParingTicketFunction(ILogger<SaveParingTicketFunction> logger)
        {
            _logger = logger;
        }

        [Function("SaveParingTicketFunction")]
        public async Task<IActionResult> SaveParingTicket([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "parkingticket/saveparkingticket")] HttpRequest req)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

                var ticket = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket>(requestBody);

                SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

                connection.Open();

                SqlCommand command = new SqlCommand("[api].[uspSaveParkingTicket]", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@ParkingTicketId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                });
                command.Parameters.AddWithValue("@ATMId", ticket.ATMId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkingTicketCode", ticket.ParkingTicketCode ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkingTicketRefrence", ticket.ParkingTicketRefrence ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkedOn", ticket.ParkedOn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkingDurationFrom", ticket.ParkingDurationFrom ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkingDurationTo", ticket.ParkingDurationTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TotalDuration", ticket.TotalDuration ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ParkingPriceId", ticket.ParkingPriceId ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@VehicleNumber", ticket.VehicleNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@PhoneNumber", ticket.PhoneNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsExtended", ticket.IsExtended ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ExtendedOn", ticket.ExtendedOn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ExtendedDurationFrom", ticket.ExtendedDurationFrom ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ExtendedDurationTo", ticket.ExtendedDurationTo ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ActualAmount", ticket.ActualAmount ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ExtendedAmount", ticket.ExtendedAmount ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@TotalAmount", ticket.TotalAmount ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Status", ticket.Status ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedBy", ticket.CreatedBy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CreatedOn", ticket.CreatedOn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ModifiedBy", ticket.ModifiedBy ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ModifiedOn", ticket.ModifiedOn ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@IsActive", ticket.IsActive);
                command.ExecuteNonQuery();
                connection.Close();

                return new OkObjectResult(Convert.ToInt64(command.Parameters["@ParkingTicketId"].Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
