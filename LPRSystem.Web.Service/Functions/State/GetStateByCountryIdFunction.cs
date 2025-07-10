using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.State;

namespace LPRSystem.Web.Service.Functions.State;

public class GetStateByCountryIdFunction
{
    private readonly ILogger<GetStateByCountryIdFunction> _logger;

    public GetStateByCountryIdFunction(ILogger<GetStateByCountryIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetStateByCountryIdFunction")]
    public IActionResult GetStateByCountryId([HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "state/getstatebycountryid/{countryid}")] HttpRequest req,
        long countryid)
    {
        try
        {
            List<StateDetails> lstStateDetails = new List<StateDetails>();

            StateDetails stateDetails = null;

            SqlConnection sqlConnection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetStateByCountryId]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@CountryId", countryid);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                stateDetails = new StateDetails();

                stateDetails.StateId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("StateId"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CountryId")))
                    stateDetails.CountryId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CountryId"));

                stateDetails.CountryName = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("CountryName"));

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

                stateDetails.IsActive = (isActiveValue != DBNull.Value && Convert.ToBoolean(isActiveValue)) ? true : false;
                lstStateDetails.Add(stateDetails);
            }

            return new OkObjectResult(lstStateDetails);
        
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}