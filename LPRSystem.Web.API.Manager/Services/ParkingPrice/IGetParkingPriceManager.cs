using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPRSystem.Web.API.Manager.Services.ParkingPrice
{
    public interface IGetParkingPriceManager
    {
        Task<IEnumerable<LPRSystem.Web.API.Manager.Models.ParkingPrice.ParkingPrice>> ExecuteAsync();
    }
}
