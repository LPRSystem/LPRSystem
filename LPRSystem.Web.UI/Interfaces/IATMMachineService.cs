using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface IATMMachineService
    {
        Task<List<ATMMachinesData>> FetchAllATMMachines();
        Task<string> InsertOrUpdateATMMachine(ATMMachine atmMachine);
    }
}
