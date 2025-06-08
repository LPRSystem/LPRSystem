using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.ParkingPlace;

namespace LPRSystem.Web.Service.Functions.ParkingPlace;

public class GetParkingPlacesFunction
{
    private readonly ILogger<GetParkingPlacesFunction> _logger;

    public GetParkingPlacesFunction(ILogger<GetParkingPlacesFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetParkingPlacesFunction")]
    public IActionResult GetParkingPlaces([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="parkingplace/getparkingplaces")] HttpRequest req)
    {
        try
        {
            List<LPRSystem.Web.API.Manager.Models.ParkingPlace.ParkingPlace> listParkingPlaces = new List<LPRSystem.Web.API.Manager.Models.ParkingPlace.ParkingPlace>();

            LPRSystem.Web.API.Manager.Models.ParkingPlace.ParkingPlace place = null;

              SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetParkingPlaces]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                place = new LPRSystem.Web.API.Manager.Models.ParkingPlace.ParkingPlace();

                place.ParkingPlaceId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingPlaceId"));

                place.ParkingPlaceName = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingPlaceName"));
                place.ParkingPlaceCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingPlaceCode"));
                place.ParkingPlaceType = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("ParkingPlaceType"));
             
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    place.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    place.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    place.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    place.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));

                object isActiveValue = sqlDataReader["IsActive"];

                place.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

                listParkingPlaces.Add(place);
            }

            return new OkObjectResult(listParkingPlaces);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}