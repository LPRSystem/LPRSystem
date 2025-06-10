using LPRSystem.Web.API.Manager.Models.PaymentMethod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class PaymentMethodConverter
    {
        public static PaymentMethod ToSinglePaymentMethod(this IDataReader reader)
        {
            PaymentMethod result = null;

            if(reader.Read())
            {
                result = new PaymentMethod();
                result.Id = reader.GetInt64(reader.GetOrdinal("Id"));

                result.Name = reader.GetString(reader.GetOrdinal("Name"));

                result.Code = reader.GetString(reader.GetOrdinal("Code"));

                result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                    result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));

                result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

                if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))

                    result.ModifiedOn = reader.GetDateTime(reader.GetOrdinal("ModifiedOn"));

                object isActiveValue = reader["IsActive"];

                result.IsActive = (isActiveValue !=DBNull.Value && isActiveValue=="1")? true : false;           
            }
            return result;
        }

        public static DataTable ToDataTable(this PaymentMethod source, string userId)
        {
            var dt = new DataTable();

            //Define the columns based on the sql table structure

            dt.Columns.Add("Id", typeof(long));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Code", typeof(string));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(string));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));

            //Add a new row with the values from the payment object and additional fields

            dt.Rows.Add(
                source.Id,
                source.Name,
                source.Code,
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
