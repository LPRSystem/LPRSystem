using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Country;

public class DeleteCountryFunction
{
    private readonly ILogger<DeleteCountryFunction> _logger;

    public DeleteCountryFunction(ILogger<DeleteCountryFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteCountryFunction")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous  , "delete", Route = "country/deletecountry/{countryid}")] HttpRequest req, long countryid)
    {
        try
        {
            bool isDeleted = false;

            string connectionString = Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting);


            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspDeleteCountry]", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@CountryId", countryid);

            int done = command.ExecuteNonQuery();

            connection.Close();

            isDeleted = done == 1 ? true : false;

            return new OkObjectResult(isDeleted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}