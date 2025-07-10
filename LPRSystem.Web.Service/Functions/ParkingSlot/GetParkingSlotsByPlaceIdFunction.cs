using LPRSystem.Web.API.Manager.Models.ParkingSlot;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingSlot;

public class GetParkingSlotsByPlaceIdFunction
{
    private readonly ILogger<GetParkingSlotsByPlaceIdFunction> _logger;

    public GetParkingSlotsByPlaceIdFunction(ILogger<GetParkingSlotsByPlaceIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetParkingSlotsByPlaceIdFunction")]
    //parkingslot/getparkingslotsbyplaceid/1
    public IActionResult GetParkingSlotsByPlaceId([HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "parkingslot/getparkingslotsbyplaceid/{placeid}")] HttpRequest req,
        long placeid)
    {
        try
        {
            List<ParkingSlotDetails> lstParkingSlotDetails = new List<ParkingSlotDetails>();

            ParkingSlotDetails parkingSlotDetails = null;

            SqlConnection sqlConnection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetParkingSlotsByPlaceId]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@ParkingPlaceId", placeid);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                parkingSlotDetails = new ParkingSlotDetails();

                parkingSlotDetails.ParkingSlotId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingSlotId"));

                parkingSlotDetails.ParkingSlotCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingSlotCode"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ParkingPlaceId")))
                    parkingSlotDetails.ParkingPlaceId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingPlaceId"));

                parkingSlotDetails.ParkingPlaceName = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingPlaceName"));

                parkingSlotDetails.ParkingPlaceCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingPlaceCode"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ATMId")))
                    parkingSlotDetails.ATMId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ATMId"));

                parkingSlotDetails.ATMCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ATMCode"));

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

                lstParkingSlotDetails.Add(parkingSlotDetails);
            }

            return new OkObjectResult(lstParkingSlotDetails);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}