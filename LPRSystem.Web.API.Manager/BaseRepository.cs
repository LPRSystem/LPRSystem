using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;

namespace LPRSystem.Web.API.Manager
{
    public class BaseRepository : DBContext
    {
        public BaseRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string tenantId, string sql, object parameters, IDbTransaction dbTransaction = null, int? commandTimeOut = Global.COMMAND_TIMEOUT_IN_SECONDS, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateConnection(tenantId))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, dbTransaction, commandTimeOut, commandType);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string tenantId, string sql, object parameters, IDbTransaction dbTransaction = null, int? commandTimeOut = Global.COMMAND_TIMEOUT_IN_SECONDS, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateConnection(tenantId))
            {
                return await connection.QueryAsync<T>(sql, parameters, dbTransaction, commandTimeOut, commandType);
            }
        }

        public async Task<T> ExecuteScalarAsync<T>(string tenantId, string sql, object parameters, IDbTransaction dbTransaction = null, int? commandTimeOut = Global.COMMAND_TIMEOUT_IN_SECONDS, CommandType commandType = CommandType.StoredProcedure)
        {
            using (var connection = CreateConnection(tenantId))
            {
                return await connection.ExecuteScalarAsync<T>(sql, parameters, dbTransaction, commandTimeOut, commandType);
            }
        }

        public async Task<T> ExecuteReaderAsync<T>(string tenantId, string sql, object parameters, IDbTransaction dbTransaction = null, int? commandTimeOut = Global.COMMAND_TIMEOUT_IN_SECONDS, CommandType commandType = CommandType.StoredProcedure)
        {
            T entity;

            using (var connection = CreateConnection(tenantId))
            using (var reader = (DbDataReader)await connection.ExecuteReaderAsync(sql, parameters, dbTransaction, commandTimeOut, commandType))
                entity = Utility.CreateEntity<T>(reader);
            return entity;
        }
    }
}