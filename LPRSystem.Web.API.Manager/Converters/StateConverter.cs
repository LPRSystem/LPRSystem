using LPRSystem.Web.API.Manager.Models.Location;
using LPRSystem.Web.API.Manager.Models.State;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
   public static class StateConverter
    {
        public static State ToSingleState(this IDataReader reader)
        {
            State result = null;
            if (reader.Read())
            {
                result = new State();
                result.StateId = reader.GetInt64(reader.GetOrdinal("StateId"));
                result.CountryId = reader.GetInt64(reader.GetOrdinal("CountryId"));
                result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
                result.Description = reader.SafeGetString(reader.GetOrdinal("Description"));
                result.StateCode = reader.SafeGetString(reader.GetOrdinal("StateCode"));
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

        public static DataTable ToDataTable(this State source, string userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("StateId", typeof(long));
            dt.Columns.Add("CountryId", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("StateCode", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.StateId,
                source.CountryId,
                source.Name,
                source.Description,
                source.StateCode,
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
