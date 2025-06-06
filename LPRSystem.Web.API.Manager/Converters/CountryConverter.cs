using LPRSystem.Web.API.Manager.Models.ATMMachine;
using LPRSystem.Web.API.Manager.Models.Country;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
     public static class CountryConverter
    {
        public static Country ToSingleCountry(this IDataReader reader)
        {
            Country result = null;
            if (reader.Read())
            {
                result = new Country();
                result.CountryId = reader.GetInt64(reader.GetOrdinal("CountryId"));
                result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
                result.Description = reader.SafeGetString(reader.GetOrdinal("Description"));
                result.CountryCode = reader.SafeGetString(reader.GetOrdinal("CountryCode"));
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

        public static DataTable ToDataTable(this Country source, string userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("CountryId", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(long));
            dt.Columns.Add("CountryCode", typeof(long));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.CountryId,
                source.Name,
                source.Description,
                source.CountryCode,
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
