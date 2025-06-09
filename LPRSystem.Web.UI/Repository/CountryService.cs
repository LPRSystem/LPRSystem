using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class CountryService : ICountryService
    {
         private HttpClient _httpClient;

        public CountryService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<bool> DeleteCountry(long countryId)
        {
            var url = Path.Combine("country/deletecountry", countryId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<bool>(responseContent);

            }
            return false;
        }

        public async Task<List<Country>> FetchAllCountries()
        {
            List<Country> countries = new List<Country>();

            var response = await _httpClient.GetAsync("country/getcountries");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                countries = JsonConvert.DeserializeObject<List<Country>>(responseContent);

            }
            return countries;
        }

        public async Task<Country> InsertOrUpdateCountry(Country country)
        {
            var inputcountry = JsonConvert.SerializeObject(country);

            var requestcountry = new StringContent(inputcountry, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("country/processcountry", requestcountry);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responcecountry = JsonConvert.DeserializeObject<Country>(content);

                return responcecountry;
            }
            return null;
        }
    }
}
