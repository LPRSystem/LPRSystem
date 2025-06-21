using LPRSystem.Web.API.Manager;
using LPRSystem.Web.API.Manager.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.Service.Functions.Authentication;

public class AuthenticationFunction
{
    private readonly ILogger<AuthenticationFunction> _logger;

    public AuthenticationFunction(ILogger<AuthenticationFunction> logger)
    {
        _logger = logger;
    }

    [Function("AuthenticationFunction")]
    public async Task<IActionResult> Authentication([HttpTrigger(AuthorizationLevel.Anonymous,
                                    "post",
                                    Route ="auth/authentication")] HttpRequest req)
    {
        _logger.LogInformation("AuthenticationFunction Invoke().");
        try
        {
            LPRSystem.Web.API.Manager.Models.User.User userInfo = null;

            LPRSystem.Web.Utility.ApplicationUser applicationUser = new LPRSystem.Web.Utility.ApplicationUser();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            // Deserialize the JSON into your request object
            var requestModel = JsonConvert.DeserializeObject<LPRSystem.Web.API.Manager.Models.Authentication.Authenticate>(requestBody);

            SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting));

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspAuthenticateUser]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@username", requestModel.Username);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                userInfo = new LPRSystem.Web.API.Manager.Models.User.User();

                userInfo.Id = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("Id"));

                userInfo.FirstName = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("FirstName"));

                userInfo.LastName = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("LastName"));

                userInfo.Email = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Email"));

                userInfo.Phone = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Phone"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("RoleId")))
                    userInfo.RoleId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("RoleId"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("RoleId")))
                    userInfo.RoleId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("RoleId"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("LastPasswordChangedOn")))
                    userInfo.LastPasswordChangedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("LastPasswordChangedOn"));

                userInfo.PasswordHash = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("PasswordHash"));

                userInfo.PasswordSalt = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("PasswordSalt"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    userInfo.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    userInfo.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    userInfo.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    userInfo.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));

                object isBlocked = sqlDataReader["IsBlocked"];

                userInfo.IsBlocked = (isBlocked != DBNull.Value && Convert.ToBoolean(isBlocked)) ? true : false;

                object isActiveValue = sqlDataReader["IsActive"];

                userInfo.IsActive = (isActiveValue != DBNull.Value && Convert.ToBoolean(isActiveValue)) ? true : false;
            }

            sqlConnection.Close();



            //login to verify user 

            if (userInfo != null)
            {
                if (userInfo.IsActive)
                {
                    if (!userInfo.IsBlocked.Value)
                    {
                        var isValidpassword = HashSalt.VerifyPassword(requestModel.Password, userInfo.PasswordHash, userInfo.PasswordSalt);


                        if (isValidpassword)
                        {
                            applicationUser.Status = true;
                            applicationUser.Message = "Ok";
                            applicationUser.Id = userInfo.Id;
                            applicationUser.Email = userInfo.Email;
                            applicationUser.Phone = userInfo.Phone;
                            applicationUser.FirstName = userInfo.FirstName;
                            applicationUser.LastName = userInfo.LastName;
                            applicationUser.FullName = userInfo.FirstName + " " + userInfo.LastName;
                        }
                        else
                        {
                            applicationUser.Status = false;
                            applicationUser.Message = "Invalid password,please try again";
                        }
                    }
                    else
                    {
                        applicationUser.Status = false;
                        applicationUser.Message = "User is bloacked";
                    }
                }
                else
                {
                    applicationUser.Status = false;
                    applicationUser.Message = "User is Inactive,please contact adming";
                }
            }
            else
            {
                applicationUser.Status = false;
                applicationUser.Message = "Unable to find the user,please check the username and retry";
            }

            return new OkObjectResult(applicationUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the City the request.");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}