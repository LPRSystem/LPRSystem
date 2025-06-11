using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.State;

public class GetStateByIdFunction
{
    private readonly ILogger<GetStateByIdFunction> _logger;

    public GetStateByIdFunction(ILogger<GetStateByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetStateByIdFunction")]
    public IActionResult GetStateById([HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "state/getstatebyid/{stateid}")] HttpRequest req,
        long stateid)
    {
        try
        {
            LPRSystem.Web.API.Manager.Models.State.State stateDetails = null;

            SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetStateById]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@StateId", stateid);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                stateDetails = new LPRSystem.Web.API.Manager.Models.State.State();

                stateDetails.StateId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("StateId"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CountryId")))
                    stateDetails.CountryId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CountryId"));

                stateDetails.Name = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Name"));

                stateDetails.Description = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Description"));

                stateDetails.StateCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("StateCode"));
                                           

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    stateDetails.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    stateDetails.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));
                
                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    stateDetails.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    stateDetails.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));

                object isActiveValue = sqlDataReader["IsActive"];

                stateDetails.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;
            }

            return new OkObjectResult(stateDetails);
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
}