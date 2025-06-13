using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class CityService : ICityService
    {
        private HttpClient _httpClient;

        public CityService() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<bool> DeleteCityAsync(long cityid)
        {
            var url = Path.Combine("city/deletecity", cityid.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(responseContent);
            }
            return false;
        }

        public async Task<List<City>> GetCityAsync()
        {
            List<City> cities = new List<City>();

            var responseContent = await _httpClient.GetAsync("city/getcity");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(response);

            }
            return cities;
        }

        public async Task<City> GetCityByIdAsync(long cityid)
        {
            City city = new City();

            var url = Path.Combine("city/getcitybyid", cityid.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                city = JsonConvert.DeserializeObject<City>(response);

            }
            return city;
        }

        public async Task<City> InsertOrUpdateCityAsync(City city)
        {
            var inputcity = JsonConvert.SerializeObject(city);

            var requestcity = new StringContent(inputcity, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("city/processcity", requestcity);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responcecity = JsonConvert.DeserializeObject<City>(content);

                return responcecity;
            }
            return null;
        }
    }
}
