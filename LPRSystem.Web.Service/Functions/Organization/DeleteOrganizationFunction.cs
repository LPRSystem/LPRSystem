using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.Organization;

public class DeleteOrganizationFunction
{
    private readonly ILogger<DeleteOrganizationFunction> _logger;

    public DeleteOrganizationFunction(ILogger<DeleteOrganizationFunction> logger)
    {
        _logger = logger;
    }

    [Function("DeleteOrganizationFunction")]
    public IActionResult DeleteOrganization([HttpTrigger(AuthorizationLevel.Anonymous, "delete",
        Route = "organization/deleteorganization/{id}")] HttpRequest req, long id)
    {

        try
        {
            using (SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;"))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("[api].[uspDeleteOrganization]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id", id);

                    int result = command.ExecuteNonQuery();

                    connection.Close();

                    return new OkObjectResult(result == 1 ? true : false);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}