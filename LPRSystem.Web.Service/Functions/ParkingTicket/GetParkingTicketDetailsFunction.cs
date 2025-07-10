using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.Service.Functions.ParkingTicket
{
    public class GetParkingTicketDetailsFunction
    {
        private readonly ILogger<GetParkingTicketDetailsFunction> _logger;

        public GetParkingTicketDetailsFunction(ILogger<GetParkingTicketDetailsFunction> logger)
        {
            _logger = logger;
        }
        [Function("GetParkingTicketDetails")]
        public IActionResult GetParkingTicketDetails([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "ParkingTicket/GetParkingTicketDetails")] HttpRequest req, string searchString, long atmId)
        {
            try
            {
                List<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket> parkingTickets = new List<LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket>();

                LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket parkingTicket = null;

                SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

                sqlConnection.Open();

                SqlCommand command = new SqlCommand("[api].[uspGetPaymentDetails]", sqlConnection);

                command.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    parkingTicket = new API.Manager.Models.ParkingTicket.ParkingTicket();

                    parkingTicket.ParkingTicketId = reader.GetInt64(reader.GetOrdinal("ParkingTicketId"));

                    parkingTicket.ATMId = reader.GetInt64(reader.GetOrdinal("AIMId"));

                    parkingTicket.ParkingTicketCode = reader.SafeGetString(reader.GetOrdinal("ParkingTicketCode"));

                    parkingTicket.ParkingTicketRefrence = reader.SafeGetString(reader.GetOrdinal("ParkingTicketRefrence"));

                    parkingTicket.ParkingPriceId = reader.GetInt64(reader.GetOrdinal("ParkingPriceId"));
                   
                    parkingTicket.VehicleNumber = reader.SafeGetString(reader.GetOrdinal("VehicleNumber"));

                    parkingTicket.PhoneNumber = reader.SafeGetString(reader.GetOrdinal("PhoneNumber"));

                    parkingTicket.Status = reader.SafeGetString(reader.GetOrdinal("Status"));

                    parkingTicket.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                    if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                        parkingTicket.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));

                    parkingTicket.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

                    if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                        parkingTicket.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

                    object isActiveValue = reader["IsActive"];

                    parkingTicket.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;


                    parkingTickets.Add(parkingTicket);
                }
                sqlConnection.Close();

                return new OkObjectResult(parkingTickets);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
