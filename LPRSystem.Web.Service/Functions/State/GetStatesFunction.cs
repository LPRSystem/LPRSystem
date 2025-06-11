using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.State;

namespace LPRSystem.Web.Service.Functions.State;

public class GetStatesFunction
{
    private readonly ILogger<GetStatesFunction> _logger;
    
    public GetStatesFunction(ILogger<GetStatesFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetStatesFunction")]
    public IActionResult GetStates([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "state/getstates")] HttpRequest req)
    {
        try
        {
            List<StateDetails> lstStateDetails = new List<StateDetails>();

            StateDetails stateDetails = null;

            SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));


            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetState]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                      

            while (sqlDataReader.Read())
            {
                stateDetails = new StateDetails();

                stateDetails.StateId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("StateId"));
                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CountryId")))
                    stateDetails.CountryId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CountryId"));
                stateDetails.CountryCode = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("CountryCode"));
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

                lstStateDetails.Add(stateDetails);
            }
            sqlConnection.Close();


            return new OkObjectResult(lstStateDetails);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}