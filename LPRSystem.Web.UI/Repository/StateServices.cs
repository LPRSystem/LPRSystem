using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Repository
{
    public class StateServices : IStateService
    {
        private readonly HttpClient _httpClient;

        public StateServices()
        {
           _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);
        }
        public async Task<bool> DeleteStateAsync(long stateId)
        {
            var url = Path.Combine("state/DeleteState", stateId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<bool>(responseContent);
            }
            return false;
        }

        public async Task<State> GetStateByIdAsync(long stateId)
        {
            State state = new State();

            var url = Path.Combine("State/GetStateByIdAsync", stateId.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                state = JsonConvert.DeserializeObject<State>(response);

            }
            return state;
        }

        public async Task<List<State>> GetStatesAync()
        {
            List<State> states = new List<State>();

            var responseContent = await _httpClient.GetAsync("State/GetStatesAync");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                states = JsonConvert.DeserializeObject<List<State>>(response);

            }
            return states;
        }

        public async Task<State> InsertOrUpdateStateAsync(State state)
        {
            var inputState = JsonConvert.SerializeObject(state);

            var requestState = new StringContent(inputState, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("State/savestate", requestState);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseState = JsonConvert.DeserializeObject<State>(content);

                return responseState;
            }
            return null;
        }

        public async Task<State> UpdateStateAsync(State state)
        {
            var inputstate = JsonConvert.SerializeObject(state);

            var requeststate = new StringContent(inputstate, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("State/UpdateState", requeststate);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responseState = JsonConvert.DeserializeObject<State>(content);

                return responseState;
            }
            return null;
        }
    }
}
