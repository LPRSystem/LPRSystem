using Azure.Core;
using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Models.Role;
using LPRSystem.Web.API.Manager.Services.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.API.Functions.Role
{
    public class RoleProcessFunction
    {
        private readonly ILogger<RoleProcessFunction> _logger;
        private readonly IRoleProcessManager _manager;
        public RoleProcessFunction(ILogger<RoleProcessFunction> logger, IRoleProcessManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Function("RoleProcess")]
        public async Task<IActionResult> RoleProcess([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<RoleProcessRequest>(requestBody);

            var processRequest = await _manager.ExecuteAsync(requestModel);

            return new OkObjectResult(processRequest);



            //List<RoleProcessRequest> processRequest = new List<RoleProcessRequest>();
            //RoleProcessRequest result = null;
            //try
            //{
            //    SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));
            //    connection.Open();
            //    SqlCommand sqlCommand = new SqlCommand("[api].[uspUpdateRole]", connection);
            //    sqlCommand.CommandType = CommandType.StoredProcedure;
            //    sqlCommand.Parameters.AddWithValue("@id", requestModel.Id);
            //    sqlCommand.Parameters.AddWithValue("@name", requestModel.Name);
            //    sqlCommand.Parameters.AddWithValue("@code", requestModel.Code);
            //    sqlCommand.Parameters.AddWithValue("@createdBy", requestModel.CreatedBy);
            //    sqlCommand.Parameters.AddWithValue("@createdOn", requestModel.CreatedOn);
            //    sqlCommand.Parameters.AddWithValue("@modifiedBy", requestModel.ModifiedBy);
            //    sqlCommand.Parameters.AddWithValue("@modifiedOn", requestModel.ModifiedOn);
            //    sqlCommand.Parameters.AddWithValue("@isActive", requestModel.IsActive ? 1 : 0);
            //    SqlDataReader reader = sqlCommand.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        result = new RoleProcessRequest();
            //        result.Id = reader.GetInt64(reader.GetOrdinal("Id"));
            //        result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
            //        result.Code = reader.SafeGetString(reader.GetOrdinal("Code"));
            //        result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
            //        if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
            //            result.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));
            //        result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
            //        if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
            //            result.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

            //        object isActiveValue = reader["IsActive"];

            //        result.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;

            //        processRequest.Add(result);
            //    }
            //    connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogInformation(ex.Message);
            //}

        }
    }
}
