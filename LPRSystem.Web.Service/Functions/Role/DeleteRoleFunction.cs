using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Role;

public class DeleteRoleFunction
{
    private readonly ILogger<DeleteRoleFunction> _logger;

    public DeleteRoleFunction(ILogger<DeleteRoleFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteRole")]
    public async Task<IActionResult> DeleteRole([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        List<RoleProcessRequest> processRequest = new List<RoleProcessRequest>();
        RoleProcessRequest result = null;
        var deleteRoleId = req.Query["roleId"].ToString();

        SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");
        connection.Open();
        SqlCommand sqlCommand = new SqlCommand("[api].[uspDeleteRole]", connection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.AddWithValue("@id", deleteRoleId);
        sqlCommand.ExecuteNonQuery();
        //SqlDataReader reader = sqlCommand.ExecuteReader();

        //while (reader.Read())
        //{
        //    result = new RoleProcessRequest();
        //    result.Id = reader.GetInt64(reader.GetOrdinal("Id"));
        //    result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
        //    result.Code = reader.SafeGetString(reader.GetOrdinal("Code"));
        //    result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
        //    if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
        //        result.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));
        //    result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
        //    if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
        //        result.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

        //    object isActiveValue = reader["IsActive"];

        //    result.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

        //    processRequest.Add(result);
        //}
        connection.Close();
        return new OkObjectResult(processRequest);
    }
}