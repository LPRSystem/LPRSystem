using LPRSystem.Web.API.Manager.Models.ParkingPrice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class ParkingPriceConverter
    {
        public static ParkingPrice ToSingleParkingPrice(this IDataReader reader)
        {
            ParkingPrice result = null;

            if (reader.Read())
            {
                result = new ParkingPrice();
                result.ParkingPriceId = reader.GetInt64(reader.GetOrdinal("ParkingPriceId"));

                result.Duration = reader.GetString(reader.GetOrdinal("Duration"));

                result.Price = reader.GetDecimal(reader.GetOrdinal("Price"));

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

        public static DataTable ToDataTable(this ParkingPrice source, string userId)
        {
            var dt = new DataTable();

            //Define the columns based on the sql table structure

            dt.Columns.Add("ParkingPriceId", typeof(long));
            dt.Columns.Add("Duration", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("CreatedOn", typeof(string));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(string));
            dt.Columns.Add("IsActive", typeof(bool));

            //Add a new row with the values from the payment object and additional fields

            dt.Rows.Add(
                source.ParkingPriceId,
                source.Duration,
                source.Price,
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
