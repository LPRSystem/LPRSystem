using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.API.Functions.User;

public class SaveUserFunction
{
    private readonly ILogger<SaveUserFunction> _logger;

    public SaveUserFunction(ILogger<SaveUserFunction> logger)
    {
        _logger = logger;
    }

    [Function("SaveUserFunction")]
    public async Task<IActionResult> SaveUser([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users/saveuser")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        // Deserialize the JSON into your request object
        var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.User.UserProcessRequest>(requestBody);

        string passwordHash = string.Empty;

        string passwordSalt = string.Empty;

        if (!string.IsNullOrEmpty(requestModel.Password))
        {
            HashSalt hashSalt = HashSalt.GenerateSaltedHash(requestModel.Password);

            passwordHash = hashSalt.Hash;
            passwordSalt = hashSalt.Salt;

        }

        string connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";

        SqlConnection sqlConnection = new SqlConnection(connectionString);
        sqlConnection.Open();
        SqlCommand sqlCommand = new SqlCommand("[api].[uspSaveUser]", sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@firstName", requestModel.FirstName);
        sqlCommand.Parameters.AddWithValue("@lastName", requestModel.LastName);
        sqlCommand.Parameters.AddWithValue("@email", requestModel.Email);
        sqlCommand.Parameters.AddWithValue("@phone", requestModel.Phone);
        sqlCommand.Parameters.AddWithValue("@passwordHash", passwordHash);
        sqlCommand.Parameters.AddWithValue("@passwordSalt", passwordSalt);
        sqlCommand.Parameters.AddWithValue("@roleId", requestModel.RoleId);
        sqlCommand.Parameters.AddWithValue("@lastPasswordChangedOn", requestModel.LastPasswordChangedOn);
        sqlCommand.Parameters.AddWithValue("@isBlocked", requestModel.IsBlocked);
        sqlCommand.Parameters.AddWithValue("@createdBy", requestModel.CreatedBy);
        sqlCommand.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
        sqlCommand.Parameters.AddWithValue("@isActive", requestModel.IsActive);
        sqlCommand.ExecuteNonQuery();

        return new OkObjectResult("Successfully user processed");
    }
}