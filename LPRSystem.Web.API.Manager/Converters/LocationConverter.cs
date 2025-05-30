using LPRSystem.Web.API.Manager.Models.Location;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class LocationConverter
    {
        public static Location ToSingleLocation(this IDataReader reader)
        {
            Location result = null;
            if (reader.Read())
            {
                result = new Location();
                result.LocationId = reader.GetInt64(reader.GetOrdinal("LocationId"));
                result.LocationName = reader.GetString(reader.GetOrdinal("LocationName"));
                result.Code = reader.GetString(reader.GetOrdinal("Code"));
                result.Address = reader.SafeGetString(reader.GetOrdinal("Address"));
                result.CountryId = reader.GetInt64(reader.GetOrdinal("CountryId"));
                result.StateId = reader.GetInt64(reader.GetOrdinal("StateId"));
                result.CityId = reader.GetInt64(reader.GetOrdinal("CityId"));
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

        public static DataTable ToDataTable(this Location source, string userId)
        {
            var dt = new DataTable();
            dt.Columns.Add("LocationId", typeof(long));
            dt.Columns.Add("LocationName", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("CountryId", typeof(long));
            dt.Columns.Add("StateId", typeof(long));
            dt.Columns.Add("CityId", typeof(long));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.LocationId,
                source.LocationName,
                source.Code,
                source.Address,
                source.CountryId,
                source.StateId,
                source.CityId,
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
