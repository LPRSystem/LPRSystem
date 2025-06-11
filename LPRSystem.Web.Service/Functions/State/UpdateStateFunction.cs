using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.State;

public class UpdateStateFunction
{
    private readonly ILogger<UpdateStateFunction> _logger;

    public UpdateStateFunction(ILogger<UpdateStateFunction> logger)
    {
        _logger = logger;
    }

    [Function("UpdateStateFunction")]
    public async Task<IActionResult> UpdateState(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put",
            Route = "state/updatestate/{stateId}")] HttpRequest req,
            long stateId)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.State.State>(requestBody);

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspUpdateState]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@stateId", requestModel.StateId);
            command.Parameters.AddWithValue("@countryId", requestModel.CountryId);
            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@description", requestModel.Description);
            command.Parameters.AddWithValue("@stateCode", requestModel.StateCode);
            command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@isActive", requestModel.IsActive);

            int response = command.ExecuteNonQuery();

            connection.Close();

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}