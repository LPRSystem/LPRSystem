using LPRSystem.Web.API.Manager.Models.PaymentMethod;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Country;

public class GetCountriesByIdFunction
{
    private readonly ILogger<GetCountriesByIdFunction> _logger;

    public GetCountriesByIdFunction(ILogger<GetCountriesByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetCountriesByIdFunction")]
    public IActionResult GetCountriesById([HttpTrigger(AuthorizationLevel.Anonymous, "get",
        Route = "country/getcountrybyid/{countryid}")] HttpRequest req, long countryid)
    {
        _logger.LogInformation("GetCountriesById Function Invoked()");

        try
        {
            if (countryid == 0)
                return new BadRequestObjectResult("Please send valid country id");

            string connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);

            LPRSystem.Web.API.Manager.Models.Country.Country country = new LPRSystem.Web.API.Manager.Models.Country.Country();

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("[api].[uspGetCountryById]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@countryId", countryid);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                country.CountryId = reader.GetInt64(reader.GetOrdinal("Id"));

                country.Name = reader.SafeGetString(reader.GetOrdinal("Name"));

                country.Description = reader.SafeGetString(reader.GetOrdinal("Description"));

                country.CountryCode = reader.SafeGetString(reader.GetOrdinal("CountryCode"));

                country.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                    country.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));

                country.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

                if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                    country.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

                object isActiveValue = reader["IsActive"];

                country.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;
            }
            connection.Close();

            return new OkObjectResult(country);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}