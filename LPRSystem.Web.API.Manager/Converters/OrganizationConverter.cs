
using LPRSystem.Web.API.Manager.Models.Organization;
using System.Data;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class OrganizationConverter
    {
        public static Organization ToSingleOrganization(this IDataReader reader)
        {
            Organization result = null;

            if (reader.Read())
            {
                result = new Organization();
                result.Id = reader.GetInt64(reader.GetOrdinal("Id"));
                result.Name = reader.SafeGetString(reader.GetOrdinal("Name"));
                result.Code = reader.SafeGetString(reader.GetOrdinal("Code"));
                result.Address = reader.SafeGetString(reader.GetOrdinal("Address"));
                result.Email = reader.SafeGetString(reader.GetOrdinal("Email"));
                result.Phone = reader.SafeGetString(reader.GetOrdinal("Phone"));
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

        public static DataTable ToDataTable(this Organization source, string userId)
        {
            var dt = new DataTable();

            // Define the columns based on the SQL table structure
            dt.Columns.Add("Id", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            // Add a new row with the values from the Role object and additional fields
            dt.Rows.Add(
                source.Id,
                source.Name,
                source.Code,
                source.Address,
                source.Email,
                source.Phone,
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
