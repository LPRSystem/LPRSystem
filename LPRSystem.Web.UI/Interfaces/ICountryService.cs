using LPRSystem.Web.UI.Models;

namespace LPRSystem.Web.UI.Interfaces
{
    public interface ICountryService
    {
        Task<List<Country>> FetchAllCountries();

        Task<Country> InsertOrUpdateCountry(Country country);

        Task<bool> DeleteCountry(long countryId);

    }
}
