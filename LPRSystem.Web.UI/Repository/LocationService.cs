using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;

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

        public async Task<List<Location>> GetLocationsAsync()
        {
            List<Location> locations = new List<Location>();

            var response = await _httpClient.GetAsync("location/getlocations");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                locations = JsonConvert.DeserializeObject<List<Location>>(responseContent);

            }
            return locations;
        }
    }
}
