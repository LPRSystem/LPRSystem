using LPRSystem.Web.API.Manager.Models.City;
using LPRSystem.Web.API.Manager.Models.Country;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class CityConverter
    {
        public static City ToSingleCity(this IDataReader reader)
        {
            City result = null;
            if (reader.Read())
            {
                result = new City();
                result.CityId = reader.GetInt64(reader.GetOrdinal("CityId"));
                result.StateId = reader.GetInt64(reader.GetOrdinal("StateId"));
                result.CountryId = reader.GetInt64(reader.GetOrdinal("CountryId"));
                result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
                result.Description = reader.SafeGetString(reader.GetOrdinal("Description"));
                result.CityCode = reader.SafeGetString(reader.GetOrdinal("CityCode"));
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

        public static DataTable ToDataTable(this City source)
        {
            var dt = new DataTable();
            dt.Columns.Add("CityId", typeof(long));
            dt.Columns.Add("StateId", typeof(long));
            dt.Columns.Add("CountryId", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Description", typeof(string));
            dt.Columns.Add("CityCode", typeof(string));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.CityId,
                source.StateId,
                source.CountryId,
                source.Name,
                source.Description,
                source.CityCode,
                DateTimeOffset.UtcNow,
                source.CreatedBy,
                DateTimeOffset.UtcNow,
                source.ModifiedBy,
                true);
            return dt;

        }
    }
}
