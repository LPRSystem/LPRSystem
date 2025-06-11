using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Models.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.Country;

public class ProcessCountryFunction
{
    private readonly ILogger<ProcessCountryFunction> _logger;

    public ProcessCountryFunction(ILogger<ProcessCountryFunction> logger)
    {
        _logger = logger;
    }

    [Function("ProcessCountryFunction")]
    public async Task<IActionResult> ProcessCountry([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "country/processcountry")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        // Deserialize the JSON into your request object
        var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Country.Country>(requestBody);

        string connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand sqlCommand = new SqlCommand("insert into [data].[country](Name,Description,CountryCode,CreatedOn,CreatedBy,ModifiedOn,ModifiedBy,IsActive)values(@name,@description,@code,@createdon,@createdby,@modifiedon,@modifiedby,@isactive)", connection);

        //sqlCommand.CommandType = CommandType.StoredProcedure;

        sqlCommand.Parameters.AddWithValue("@name", requestModel.Name);
        sqlCommand.Parameters.AddWithValue("@description", requestModel.Description);
        sqlCommand.Parameters.AddWithValue("@code", requestModel.CountryCode);
        sqlCommand.Parameters.AddWithValue("@createdon", requestModel.CreatedOn);
        sqlCommand.Parameters.AddWithValue("@createdby", requestModel.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@modifiedon", requestModel.ModifiedOn);
        sqlCommand.Parameters.AddWithValue("@modifiedby", requestModel.ModifiedBy);
        sqlCommand.Parameters.AddWithValue("@isactive", requestModel.IsActive);
        sqlCommand.ExecuteNonQuery();
        connection.Close();

        return new OkObjectResult("Welcome to Azure Functions!");
    }
}