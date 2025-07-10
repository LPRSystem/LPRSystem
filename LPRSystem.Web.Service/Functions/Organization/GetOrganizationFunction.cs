using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Organization;

public class GetOrganizationFunction
{
    private readonly ILogger<GetOrganizationFunction> _logger;

    public GetOrganizationFunction(ILogger<GetOrganizationFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetOrganizationFunction")]
    public IActionResult GetOrganization([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "organization/getorganization")] HttpRequest req)
    {
        try
        {
            List<LPRSystem.Web.API.Manager.Models.Organization.Organization> lstOrganization = new List<API.Manager.Models.Organization.Organization>();

            LPRSystem.Web.API.Manager.Models.Organization.Organization organization = null;

            SqlConnection sqlConnection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");


            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetOrganization]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();


            while (sqlDataReader.Read())
            {
                organization = new API.Manager.Models.Organization.Organization();

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
                organization.IsActive = (isActiveValue != DBNull.Value) && Convert.ToBoolean(isActiveValue);
                //organization.IsActive = (isActiveValue != DBNull.Value && Convert.ToInt32(isActiveValue) == 1);

                lstOrganization.Add(organization);
            }
            sqlConnection.Close();


            return new OkObjectResult(lstOrganization);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
