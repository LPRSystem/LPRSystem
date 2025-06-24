using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Organization;

public class GetOrganizationByIdFunction
{
    private readonly ILogger<GetOrganizationByIdFunction> _logger;

    public GetOrganizationByIdFunction(ILogger<GetOrganizationByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetOrganizationByIdFunction")]
    public IActionResult GetOrganizationById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organization/getorganizationbyid/{id}")] HttpRequest req, long id)
    {
        try
        {
            LPRSystem.Web.API.Manager.Models.Organization.Organization organization = new LPRSystem.Web.API.Manager.Models.Organization.Organization();


            SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));


            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetOrganizationById]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            sqlCommand.Parameters.AddWithValue("@Id", id);

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();


            while (sqlDataReader.Read())
            {

                organization.Id = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("Id"));
                organization.Name = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Name"));
                organization.Code = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Code"));
                organization.Address = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Address"));
                organization.Email = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Email"));
                organization.Phone = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Phone"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    organization.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));
                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    organization.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    organization.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));
                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    organization.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));
                object isActiveValue = sqlDataReader["IsActive"];
                organization.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;


            }
            sqlConnection.Close();


            return new OkObjectResult(organization);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}