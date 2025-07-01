using System.Data;

namespace LPRSystem.Web.API.Manager.Converters
{
    public static class ParkingTicketConverter
    {
        public static LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket ToSingleParkingTicket(this IDataReader reader)
        {
            LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket result = null;
            if (reader.Read())
            {
                result = new LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket();
                result.ATMId = reader.GetInt64(reader.GetOrdinal("ATMId"));
                result.ParkingTicketCode = reader.SafeGetString(reader.GetOrdinal("ParkingTicketCode"));
                result.ParkingTicketRefrence = reader.SafeGetString(reader.GetOrdinal("ParkingTicketRefrence"));
                result.ParkedOn = reader.GetDateTime(reader.GetOrdinal("ParkedOn"));
                result.ParkingDurationFrom = reader.GetDateTime(reader.GetOrdinal("ParkingDurationFrom"));
                result.ParkingDurationTo = reader.GetDateTime(reader.GetOrdinal("ParkingDurationTo"));
                result.TotalDuration = reader.GetInt64(reader.GetOrdinal("TotalDuration"));
                result.ParkingPriceId = reader.GetInt64(reader.GetOrdinal("ParkingPriceId"));
                result.VehicleNumber = reader.SafeGetString(reader.GetOrdinal("VehicleNumber"));
                result.PhoneNumber = reader.SafeGetString(reader.GetOrdinal("PhoneNumber"));
                object isExtendedValue = reader["IsExtended"];
                result.IsExtended = (isExtendedValue != DBNull.Value && Convert.ToBoolean(isExtendedValue))? true: false;
                result.ExtendedOn = reader.GetDateTime(reader.GetOrdinal("ExtendedOn"));
                result.ExtendedDurationFrom = reader.GetDateTime(reader.GetOrdinal("ExtendedDurationFrom"));
                result.ExtendedDurationTo = reader.GetDateTime(reader.GetOrdinal("ExtendedDurationTo"));
                result.ActualAmount = reader.GetDecimal(reader.GetOrdinal("ActualAmount"));
                result.ExtendedAmount = reader.GetDecimal(reader.GetOrdinal("ExtendedAmount"));
                result.TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount"));
                result.Status = reader.SafeGetString(reader.GetOrdinal("Status"));
                result.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));
                if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                    result.CreatedOn = reader.GetDateTime(reader.GetOrdinal("CreatedOn"));
                result.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));
                if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                    result.ModifiedOn = reader.GetDateTime(reader.GetOrdinal("ModifiedOn"));
                object isActiveValue = reader["IsActive"];
                result.IsActive = (isActiveValue != DBNull.Value && Convert.ToBoolean(isActiveValue)) ? true : false;
            }
            return result;
        }

        public static DataTable ToDataTable(this LPRSystem.Web.API.Manager.Models.ParkingTicket.ParkingTicket source)
        {
            var dt = new DataTable();
            dt.Columns.Add("ParkingTicketId", typeof(long));
            dt.Columns.Add("ParkingTicketCode", typeof(string));
            dt.Columns.Add("ParkingTicketRefrence", typeof(string));
            dt.Columns.Add("ParkedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ParkingDurationFrom", typeof(DateTimeOffset));
            dt.Columns.Add("ParkingDurationTo", typeof(DateTimeOffset));
            dt.Columns.Add("TotalDuration", typeof(long));
            dt.Columns.Add("ParkingPriceId", typeof(long));
            dt.Columns.Add("VehicleNumber", typeof(string));
            dt.Columns.Add("PhoneNumber", typeof(string));
            dt.Columns.Add("IsExtended", typeof(bool));
            dt.Columns.Add("ExtendedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ExtendedDurationFrom", typeof(DateTimeOffset));
            dt.Columns.Add("ExtendedDurationTo", typeof(DateTimeOffset));
            dt.Columns.Add("ActualAmount", typeof(decimal));
            dt.Columns.Add("ExtendedAmount", typeof(decimal));
            dt.Columns.Add("TotalAmount", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("CreatedOn", typeof(DateTimeOffset));
            dt.Columns.Add("CreatedBy", typeof(long));
            dt.Columns.Add("ModifiedOn", typeof(DateTimeOffset));
            dt.Columns.Add("ModifiedBy", typeof(long));
            dt.Columns.Add("IsActive", typeof(bool));

            dt.Rows.Add(
                source.ParkingTicketId,
                source.ParkingTicketCode,
                source.ParkingTicketRefrence,
                source.ParkedOn,
                source.ParkingDurationFrom,
                source.ParkingDurationTo,
                source.TotalDuration,
                source.ParkingPriceId,
                source.VehicleNumber,
                source.PhoneNumber,
                source.IsExtended,
                source.ExtendedOn,
                source.ExtendedDurationFrom,
                source.ExtendedDurationTo,
                source.ActualAmount,
                source.ExtendedAmount,
                source.TotalAmount,
                source.Status,
                DateTimeOffset.UtcNow,
                source.CreatedBy,
                DateTimeOffset.UtcNow,
                source.ModifiedBy,
                true);
            return dt;

        }
    }
}
