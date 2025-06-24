using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Organization;

public class SaveOrganizationFunction
{
    private readonly ILogger<SaveOrganizationFunction> _logger;

    public SaveOrganizationFunction(ILogger<SaveOrganizationFunction> logger)
    {
        _logger = logger;
    }

    [Function("SaveOrganizationFunction")]
    public async Task<IActionResult> InsertOrganization([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "organization/Saveorganization")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Organization.Organization>(requestBody);

            SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspInsertOrganization]", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@code", requestModel.Code);
            command.Parameters.AddWithValue("@address", requestModel.Address);
            command.Parameters.AddWithValue("@email", requestModel.Email);
            command.Parameters.AddWithValue("@phone", requestModel.Phone);
            command.Parameters.AddWithValue("@createdBy", requestModel.CreatedBy);
            command.Parameters.AddWithValue("@createdOn", requestModel.CreatedOn);
            command.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            command.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn);

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