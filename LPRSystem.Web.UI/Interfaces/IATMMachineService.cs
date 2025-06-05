using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IATMMachineService
    {
        Task<List<ATMMachinesData>> FetchAllATMMachines();
        Task<ATMMachine> InsertOrUpdateATMMachine(ATMMachine atmMachine);
        Task<bool> DeleteATMMachineAsync(long atmId);
    }
}
