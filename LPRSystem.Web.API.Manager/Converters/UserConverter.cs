using LPRSystem.Web.API.Manager.Models.Organization;
using LPRSystem.Web.API.Manager.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class UserConverter
    {
        public static User ToSingleUser(this IDataReader reader)
        {
            User result = null;
            if (reader.Read())
            {
                result = new User();
                result.Id = reader.GetInt64(reader.GetOrdinal("Id"));
                result.FirstName = reader.SafeGetString(reader.GetOrdinal("FirstName"));
                result.LastName = reader.SafeGetString(reader.GetOrdinal("LastName"));
                result.Email = reader.SafeGetString(reader.GetOrdinal("Email"));
                result.Phone = reader.SafeGetString(reader.GetOrdinal("Phone"));
                result.PasswordHash = reader.SafeGetString(reader.GetOrdinal("PasswordHash"));
                result.PasswordSalt = reader.SafeGetString(reader.GetOrdinal("PasswordSalt"));
                result.RoleId = reader.GetInt64(reader.GetOrdinal("RoleId"));
                result.IsBlocked = reader.GetBoolean(reader.GetOrdinal("IsBlocked"));
                result.LastPasswordChangedOn = reader.GetDateTime(reader.GetOrdinal("LastPasswordChangedOn"));
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
        public static DataTable ToDataTable(this User source, string userId)
        {
            var dt = new DataTable();

            // Define the columns based on the SQL table structure
            dt.Columns.Add("Id", typeof(long));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("PasswordHash", typeof(string));
            dt.Columns.Add("PasswordSalt", typeof(string));
            dt.Columns.Add("RoleId", typeof(long));
            dt.Columns.Add("IsBlocked", typeof(bool));
            dt.Columns.Add("LastPasswordChangedOn", typeof(DateTimeOffset));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("IsActive", typeof(bool));

            // Add a new row with the values from the Role object and additional fields
            dt.Rows.Add(
                source.Id,                                   // Id
                source.FirstName,                            // FirstName
                source.LastName,                             // LastName
                source.Email,                                // Email
                source.Phone,                                // Phone
                source.PasswordHash,                         // PasswordHash
                source.PasswordSalt,                         // PasswordSalt
                source.RoleId,                               // RoleId
                source.IsBlocked,                            // IsBlocked
                source.LastPasswordChangedOn,                // LastPasswordChangedOn
                long.Parse(userId),                          // CreatedBy
                DateTimeOffset.UtcNow,                       // CreatedOn
                long.Parse(userId),                          // ModifiedBy
                DateTimeOffset.UtcNow,                       // ModifiedOn
                true                                         // IsActive
            );


            return dt;
        }
    }
}
