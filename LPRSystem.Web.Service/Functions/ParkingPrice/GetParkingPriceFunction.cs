using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class GetParkingPriceFunction
{
    private readonly ILogger<GetParkingPriceFunction> _logger;

    public GetParkingPriceFunction(ILogger<GetParkingPriceFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetParkingPriceFunction")]
    public IActionResult GetParkingPrice([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="parkingprice/getparkingprice")] HttpRequest req)
    {
        try
        {
            List<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice> listParkingPrice = new List<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice>();

            LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice price = null;

            SqlConnection sqlConnection = new SqlConnection(Environment.GetEnvironmentVariable(Global.CommonSQLServerConnectionStringSetting));

            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand("[api].[uspGetParkingPrice]", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                price = new LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice();

                price.ParkingPriceId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ParkingPriceId"));

                price.Duration = sqlDataReader.SafeGetString(sqlDataReader.GetOrdinal("Duration"));
                price.Price = sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("Price"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("CreatedBy")))
                    price.CreatedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("CreatedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("CreatedOn")))
                    price.CreatedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("CreatedOn"));

                if (!sqlDataReader.IsDBNull(sqlDataReader.GetOrdinal("ModifiedBy")))
                    price.ModifiedBy = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ModifiedBy"));

                if (sqlDataReader.IsSafe(sqlDataReader.GetOrdinal("ModifiedOn")))
                    price.ModifiedOn = sqlDataReader.GetDateTimeOffset(sqlDataReader.GetOrdinal("ModifiedOn"));

                object isActiveValue = sqlDataReader["IsActive"];

                price.IsActive = (isActiveValue != DBNull.Value) && Convert.ToBoolean(isActiveValue);

                listParkingPrice.Add(price);
            }

            return new OkObjectResult(listParkingPrice);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}