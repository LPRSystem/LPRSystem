using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;

namespace LPRSystem.Web.UI.Repository
{
    public class ParkingPriceService : IParkingPriceService
    {
        private readonly HttpClient _httpClient;
        public ParkingPriceService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }
        public async Task<List<ParkingPrice>> GetParkingPriceListAsync()
        {
            List<ParkingPrice> parkingPrice = new List<ParkingPrice>();

            var response = await _httpClient.GetAsync("parkingprice/getparkingprice");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                parkingPrice = JsonConvert.DeserializeObject<List<ParkingPrice>>(responseContent);
            }

            return parkingPrice;
        }
    }
}
