using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LPRSystem.Web.Service.Functions.Country;

public class GetCountriesFunction
{
    private readonly ILogger<GetCountriesFunction> _logger;

    public GetCountriesFunction(ILogger<GetCountriesFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetCountriesFunction")]
    public async Task<IActionResult> GetCountries([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "countries/getcountries")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = new SqlCommand("[api].[uspGetCountries]", connection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        SqlDataReader reader = sqlCommand.ExecuteReader();

        List<LPRSystem.Web.API.Manager.Models.Country.Country> countries = new List<API.Manager.Models.Country.Country>();

        LPRSystem.Web.API.Manager.Models.Country.Country result = null;

        while (reader.Read())
        {
            result = new LPRSystem.Web.API.Manager.Models.Country.Country();
            result.CountryId = reader.GetInt64(reader.GetOrdinal("CountryId"));
            result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
            result.Description = reader.SafeGetString(reader.GetOrdinal("Description"));
            result.CountryCode = reader.SafeGetString(reader.GetOrdinal("CountryCode"));
            result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
            if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                result.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));
            result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
            if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                result.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

            object isActiveValue = reader["IsActive"];

            result.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

            countries.Add(result);
        }
        connection.Close();


        return new OkObjectResult(countries);
    }
    
}