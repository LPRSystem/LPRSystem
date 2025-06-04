using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class ATMMachineService : IATMMachineService
    {
        private HttpClient _httpClient;

        public ATMMachineService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }

        public async Task<List<ATMMachinesData>> FetchAllATMMachines()
        {
            List<ATMMachinesData> aTMMachines = new List<ATMMachinesData>();

            var response = await _httpClient.GetAsync("atmmachine/getatmmachines");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                aTMMachines = JsonConvert.DeserializeObject<List<ATMMachinesData>>(responseContent);

            }
            return aTMMachines;
        }

        public async Task<string> InsertOrUpdateATMMachine(ATMMachine atmMachine)
        {
            var inputatmMachine = JsonConvert.SerializeObject(atmMachine);

            var requestatmMachine = new StringContent(inputatmMachine, Encoding.UTF8, "application/json");

            var responce = await _httpClient.PostAsync("atmmachine/processatmmachine", requestatmMachine);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceatmMachine = JsonConvert.DeserializeObject<string>(content);

                return responceatmMachine;
            }
            return string.Empty;
        }
    }
}
