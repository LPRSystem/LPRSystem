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
                connectionString = Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting);
            }
            else
            {
                connectionString = string.Format(Environment.GetEnvironmentVariable(Global.TenantSQLServerConnectionStringSetting), tenantId);
            }
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Invalid connection for tenant {tenantId}");

            }
            return new SqlConnection(connectionString);
        }
    }
}
