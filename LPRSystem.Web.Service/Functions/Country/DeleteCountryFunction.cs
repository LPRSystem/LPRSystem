using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.PaymentMethod;

namespace LPRSystem.Web.Service.Functions.Country;

public class DeleteCountryFunction
{
    private readonly ILogger<DeleteCountryFunction> _logger;

    public DeleteCountryFunction(ILogger<DeleteCountryFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteCountryFunction")]
    public async Task< IActionResult> DeleteCountry([HttpTrigger(AuthorizationLevel.Anonymous  , "delete", 
        Route = "country/deletecountry/{countryid}")] HttpRequest req, long countryid)
    {
        _logger.LogInformation("Delete Country method invoked.");

        bool deleted = false;

        SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

        connection.Open();

        SqlCommand sqlCommand = new SqlCommand("[api].[uspDeleteCountry]", connection);

        sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.Parameters.AddWithValue("@id", countryid);

        var response = sqlCommand.ExecuteNonQuery();

        connection.Close();

        deleted = response == 1 ? true : false;

        return new OkObjectResult(deleted);
    }
}