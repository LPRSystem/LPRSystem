using LPRSystem.Web.API.Manager.Services.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingPrice
{
    public class GetParkingPriceManager:IGetParkingPriceManager
    {
        private static IGetParkingPriceRepository _repository;
        public GetParkingPriceManager(IGetParkingPriceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.ParkingPrice.ParkingPrice>> ExecuteAsync()
        {
            return await _repository.ExecuteAsync();
        }
    }
}
