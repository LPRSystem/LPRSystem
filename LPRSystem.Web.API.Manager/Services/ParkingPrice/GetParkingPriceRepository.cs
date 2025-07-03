using LPRSystem.Web.API.Manager.Constants;
using LPRSystem.Web.API.Manager.Services.State;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingPrice
{
    public class GetParkingPriceRepository : BaseRepository, IGetParkingPriceRepository
    {
         
            public GetParkingPriceRepository(IConfiguration configuration) : base(configuration)
            {

            }

            public async Task<IEnumerable<Models.ParkingPrice.ParkingPrice>> ExecuteAsync()
            {
                return await base.QueryAsync<Models.ParkingPrice.ParkingPrice>(CommonConstants.CommonDB, ParkingPriceConstant.GetParkingPrice, null, null);
            }

       
    }
}
