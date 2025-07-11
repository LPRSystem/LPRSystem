﻿using LPRSystem.Web.UI.Interfaces;
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

        public async Task<StateDetails> GetStateByIdAsync(long stateId)
        {
            StateDetails state = new StateDetails();

            var url = Path.Combine("State/GetStateByIdAsync", stateId.ToString());

            var responseContent = await _httpClient.GetAsync(url);

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                state = JsonConvert.DeserializeObject<StateDetails>(response);

            }
            return state;
        }

        public async Task<List<StateDetails>> GetStatesAync()
        {
            List<StateDetails> states = new List<StateDetails>();

            var responseContent = await _httpClient.GetAsync("State/GetStates");

            if (responseContent.IsSuccessStatusCode)
            {
                var response = await responseContent.Content.ReadAsStringAsync();

                states = JsonConvert.DeserializeObject<List<StateDetails>>(response);

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
            //combine the url and query params in the url
            var url = Path.Combine("State/UpdateState", state.StateId.ToString());

            //prepare the incoming object as json string

            var inputstate = JsonConvert.SerializeObject(state);

            var requeststate = new StringContent(inputstate, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, requeststate);

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
