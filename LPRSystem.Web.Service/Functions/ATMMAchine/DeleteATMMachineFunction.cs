using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.ATMMAchine;

public class DeleteATMMachineFunction
{
    private readonly ILogger<DeleteATMMachineFunction> _logger;

    public DeleteATMMachineFunction(ILogger<DeleteATMMachineFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteATMMachineFunction")]
    public IActionResult DeleteATMMachine([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
        Route = "atmmachine/deleteatmmachine/{atmid}")] HttpRequest req, long atmid)
    {
        try
        {
            bool isDeleted = false;

            string connectionString = Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting);


            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspDeleteATMMachine]", connection);

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@ATMId", atmid);

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