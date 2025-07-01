using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IParkingPriceService
    {
        Task<List<ParkingPrice>> GetParkingPriceListAsync();
    }
}
