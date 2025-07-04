using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.ParkingTicket
{
    public class GetParkingTicketByIdFunction
    {
        private readonly ILogger<GetParkingTicketByIdFunction> _logger;

        public GetParkingTicketByIdFunction(ILogger<GetParkingTicketByIdFunction> logger)
        {
            _logger = logger;
        }

        [Function("GetParkingTicketByIdFunction")]
        public async Task<IActionResult> GetParkingTicketById([HttpTrigger(AuthorizationLevel.Anonymous, "get",
            Route = "parkingticket/getallparkingticketbyid/{parkingticketid}")] HttpRequest req, long parkingticketid)
        {
            _logger.LogInformation("GetParkingPriceById Function Invoked()");

            try
            {
                if (parkingticketid == 0)
                    return new BadRequestObjectResult("Please send valid payment id");

                LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket result = new API.Manager.Models.ParkingTicket.ParkingTicket();

                SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));
                connection.Open();
                SqlCommand command = new SqlCommand("[api].[uspGetParkingTicketById]", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ParkingTicketId", parkingticketid);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.ParkingTicketId = reader.GetInt64(reader.GetOrdinal("ParkingTicketId"));
                    result.ATMId = reader.GetInt64(reader.GetOrdinal("ATMId"));
                    result.ParkingTicketCode = reader.SafeGetString(reader.GetOrdinal("ParkingTicketCode"));
                    result.ParkingTicketRefrence = reader.SafeGetString(reader.GetOrdinal("ParkingTicketRefrence"));
                    if (reader.IsSafe(reader.GetOrdinal("ParkedOn")))
                        result.ParkedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ParkedOn"));
                    if (reader.IsSafe(reader.GetOrdinal("ParkingDurationFrom")))
                        result.ParkingDurationFrom = reader.GetDateTimeOffset(reader.GetOrdinal("ParkingDurationFrom"));
                    if (reader.IsSafe(reader.GetOrdinal("ParkingDurationTo")))
                        result.ParkingDurationTo = reader.GetDateTimeOffset(reader.GetOrdinal("ParkingDurationTo"));

                    result.TotalDuration = reader.GetInt64(reader.GetOrdinal("TotalDuration"));

                    result.ParkingPriceId = reader.GetInt64(reader.GetOrdinal("ParkingPriceId"));

                    result.VehicleNumber = reader.SafeGetString(reader.GetOrdinal("VehicleNumber"));

                    result.PhoneNumber = reader.SafeGetString(reader.GetOrdinal("PhoneNumber"));

                    object isExtendedValue = reader["IsExtended"];

                    result.IsExtended = (isExtendedValue != DBNull.Value && Convert.ToBoolean(isExtendedValue)) ? true : false;
                    if (reader.IsSafe(reader.GetOrdinal("ExtendedOn")))
                        result.ExtendedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ExtendedOn"));
                    if (reader.IsSafe(reader.GetOrdinal("ExtendedDurationFrom")))
                        result.ExtendedDurationFrom = reader.GetDateTimeOffset(reader.GetOrdinal("ExtendedDurationFrom"));
                    if (reader.IsSafe(reader.GetOrdinal("ExtendedDurationTo")))
                        result.ExtendedDurationTo = reader.GetDateTimeOffset(reader.GetOrdinal("ExtendedDurationTo"));

                    result.ActualAmount = reader.GetDecimal(reader.GetOrdinal("ActualAmount"));

                    result.ExtendedAmount = reader.GetDecimal(reader.GetOrdinal("ExtendedAmount"));

                    result.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));

                    result.Status = reader.SafeGetString(reader.GetOrdinal("Status"));

                    result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                    if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                        result.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));
                    result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
                    if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                        result.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));
                    object isActiveValue = reader["IsActive"];
                    result.IsActive = (isActiveValue != DBNull.Value && Convert.ToBoolean(isActiveValue)) ? true : false;
                }
                connection.Close();

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
