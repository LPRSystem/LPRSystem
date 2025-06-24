using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class LocationService : ILocationService
    {
        private HttpClient _httpClient;

        public LocationService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<List<LocationVM>> GetLocationsAsync()
        {
            List<LocationVM> locations = new List<LocationVM>();

            var response = await _httpClient.GetAsync("location/getlocations");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                locations = JsonConvert.DeserializeObject<List<LocationVM>>(responseContent);

            }
            return locations;
        }

        public async Task<Location> InsertOrUpdateLocation(Location location)
        {
            var inputlocation = JsonConvert.SerializeObject(location);

            var requestlocation = new StringContent(inputlocation, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("location/processlocation", requestlocation);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responcelocation = JsonConvert.DeserializeObject<Location>(content);

                return responcelocation;
            }
            return null;
        }
    }
}
