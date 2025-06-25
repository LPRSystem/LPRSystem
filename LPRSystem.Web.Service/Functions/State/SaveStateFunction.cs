using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using LPRSystem.Web.API.Manager.Models.Country;
using Grpc.Core;

namespace LPRSystem.Web.Service.Functions.State;

public class SaveStateFunction
{
    private readonly ILogger<SaveStateFunction> _logger;

    public SaveStateFunction(ILogger<SaveStateFunction> logger)
    {
        _logger = logger;
    }

    [Function("SaveStateFunction")]
    public async Task<IActionResult> InsertState([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "state/Savestate")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //www.google.com/id?1000%name=prasad&age=10
            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.State.State>(requestBody);

            if (requestModel == null || requestModel.CountryId == 0)
            {
                return new BadRequestObjectResult("Invalid input. CountryId is required.");
            }

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspInsertState]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@countryId", requestModel.CountryId);
            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@description", requestModel.Description);
            command.Parameters.AddWithValue("@stateCode", requestModel.StateCode);
            command.Parameters.AddWithValue("@createdOn", requestModel.CreatedOn);
            command.Parameters.AddWithValue("@createdBy", requestModel.CreatedBy);
            command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn);
            command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@isActive", requestModel.IsActive);

            int response = command.ExecuteNonQuery();

            connection.Close();

            return new OkObjectResult(requestModel);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}