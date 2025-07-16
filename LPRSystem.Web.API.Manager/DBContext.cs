using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace LPRSystem.Web.API.Manager
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;
        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection(string tenantId)
        {
            string connectionString = string.Empty;

            if (string.IsNullOrEmpty(tenantId))
            {
                connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";
            }
            else
            {
                connectionString = "Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;";
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Invalid connection for tenant {tenantId}");

            }
            return new SqlConnection(connectionString);
        }
    }
}
