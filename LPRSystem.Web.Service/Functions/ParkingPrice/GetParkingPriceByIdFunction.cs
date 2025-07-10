using LPRSystem.Web.API.Manager.Models.PaymentMethod;
using LPRSystem.Web.API.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Data;

namespace LPRSystem.Web.Service.Functions.ParkingPrice;

public class GetParkingPriceByIdFunction
{
    private readonly ILogger<GetParkingPriceByIdFunction> _logger;

    public GetParkingPriceByIdFunction(ILogger<GetParkingPriceByIdFunction> logger)
    {
        _logger = logger;
    }

    [Function("GetParkingPriceByIdFunction")]
    public IActionResult GetParkingPriceById([HttpTrigger(AuthorizationLevel.Anonymous,
        "get", 
        Route ="parkingprice/getparkingpricebyid{parkingpriceid}")] HttpRequest req, 
        long parkingpriceid)
    {
        _logger.LogInformation("GetParkingPriceById Function Invoked()");

        try
        {
            if (parkingpriceid == 0)
                return new BadRequestObjectResult("Please send valid payment id");

            LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice parkingPrice = new LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice();

            SqlConnection connection = new SqlConnection("Data Source=104.243.32.43;Initial Catalog=LPRSystemDB;User ID=LPRSystemDBUser;Password=DubaiDutyFree@2025;TrustServerCertificate=true;");
            connection.Open();
            SqlCommand command = new SqlCommand("[api].[uspGetParkingPriceById]", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@parkingPriceId", parkingpriceid);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                parkingPrice.ParkingPriceId = reader.GetInt64(reader.GetOrdinal("ParkingPriceId"));

                parkingPrice.Duration = reader.SafeGetString(reader.GetOrdinal("Duration"));

                parkingPrice.Price = reader.GetDecimal(reader.GetOrdinal("Price"));

                parkingPrice.CreatedBy = reader.GetInt64(reader.GetOrdinal("CreatedBy"));

                if (reader.IsSafe(reader.GetOrdinal("CreatedOn")))
                    parkingPrice.CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));

                parkingPrice.ModifiedBy = reader.GetInt64(reader.GetOrdinal("ModifiedBy"));

                if (reader.IsSafe(reader.GetOrdinal("ModifiedOn")))
                    parkingPrice.ModifiedOn = reader.GetDateTimeOffset(reader.GetOrdinal("ModifiedOn"));

                object isActiveValue = reader["IsActive"];

                parkingPrice.IsActive = (isActiveValue != DBNull.Value) && Convert.ToBoolean(isActiveValue);
            }
            connection.Close();

            return new OkObjectResult(parkingPrice);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}