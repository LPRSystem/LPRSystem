using LPRSystem.Web.API.Manager.Models.ATMMachine;
using LPRSystem.Web.API.Manager.Models.Location;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class ATMMAchineConverter
    {
        public static ATMMachine ToSingleATMMachine (this IDataReader reader) 
        {
            ATMMachine result = null;
            if (reader.Read())
            {
                result = new ATMMachine();
                result.ATMId = reader.GetInt64(reader.GetOrdinal("ATMId"));
                result.ATMCode = reader.SafeGetString(reader.GetOrdinal("ATMCode"));
                result.LocationId = reader.GetInt64(reader.GetOrdinal("LocationId"));
                result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
                if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                    result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
                result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
                if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                    result.ModifiedOn = reader.GetDateTime(reader.GetOrdinal("ModifiedOn"));
                object isActiveValue = reader["IsActive"];
                result.IsActive = (isActiveValue != DBNull.Value && isActiveValue == "1") ? true : false;
            }
            return result;
        }

        public static DataTable ToDataTable(this ATMMachine source, string userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("ATMId", typeof(long));
            dt.Columns.Add("ATMCode", typeof(string));
            dt.Columns.Add("LocationId", typeof(long));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.ATMId,
                source.ATMCode,
                source.LocationId,
                long.Parse(userId),
                DateTimeOffset.UtcNow,
                long.Parse(userId),
                DateTimeOffset.UtcNow,
                true
                );
            return dt;

        }
    }
}
