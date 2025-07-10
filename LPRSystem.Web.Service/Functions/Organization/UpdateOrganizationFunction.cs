using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Organization;

public class UpdateOrganizationFunction
{
    private readonly ILogger<UpdateOrganizationFunction> _logger;

    public UpdateOrganizationFunction(ILogger<UpdateOrganizationFunction> logger)
    {
        _logger = logger;
    }

    [Function("UpdateOrganizationFunction")]
    public async Task<IActionResult> OrganizationOrganization([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "organization/updateorganization/{id}")] HttpRequest req)
    {
        try
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Organization.Organization>(requestBody);

            SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");

            connection.Open();

            SqlCommand command = new SqlCommand("[api].[uspUpdateOrganization]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", requestModel.Id);
            command.Parameters.AddWithValue("@name", requestModel.Name);
            command.Parameters.AddWithValue("@code", requestModel.Code);
            command.Parameters.AddWithValue("@address", requestModel.Address);
            command.Parameters.AddWithValue("@email", requestModel.Email);
            command.Parameters.AddWithValue("@phone", requestModel.Phone);
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