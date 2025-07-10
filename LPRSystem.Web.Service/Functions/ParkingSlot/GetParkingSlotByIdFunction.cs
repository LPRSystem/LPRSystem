using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingSlot;

public class GetParkingSlotByIdFunction
{
    private readonly ILogger<GetParkingSlotByIdFunction> _logger;

    public GetParkingSlotByIdFunction(ILogger<GetParkingSlotByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetParkingSlotByIdFunction")]
    public IActionResult GetParkingSlotById([HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "parkingslot/getparkingslotbyid/{parkingslotid}")] HttpRequest req,
        long parkingslotid)
    {
        try
        {
            LPRSystem.Web.API.Manager.Models.ParkingSlot.ParkingSlot parkingSlotDetails = null;

            SqlConnection sqlConnection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetParkingSlotById]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ParkingSlotId", parkingslotid);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                parkingSlotDetails = new LPRSystem.Web.API.Manager.Models.ParkingSlot.ParkingSlot();

                parkingSlotDetails.ParkingSlotId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingSlotId"));

                parkingSlotDetails.ParkingSlotCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingSlotCode"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ParkingPlaceId")))
                    parkingSlotDetails.ParkingPlaceId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingPlaceId"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ATMId")))
                    parkingSlotDetails.ATMId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ATMId"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    parkingSlotDetails.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    parkingSlotDetails.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    parkingSlotDetails.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    parkingSlotDetails.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));

                object isActiveValue = sqlDataReader["IsActive"];

                parkingSlotDetails.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;
            }

            return new OkObjectResult(parkingSlotDetails);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}