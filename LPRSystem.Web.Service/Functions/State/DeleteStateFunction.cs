using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.State;

public class DeleteStateFunction
{
    private readonly ILogger<DeleteStateFunction> _logger;

    public DeleteStateFunction(ILogger<DeleteStateFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteStateFunction")]
    public IActionResult DeleteState([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
        Route = "state/deletestate/{stateId}")] HttpRequest req, long stateId)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting)))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[api].[uspDeleteState]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@stateId", stateId);

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    return new OkObjectResult(result == 1 ? true : false);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}