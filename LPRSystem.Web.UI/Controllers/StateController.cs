using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace LPRSystem.Web.UI.Controllers
{
    public class StateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyfService;
        public StateController(INotyfService notyfService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);

            _notyfService = notyfService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<StateDetails> stateDetails = new List<StateDetails>();

            var response = await _httpClient.GetAsync("state/getstates");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                stateDetails = JsonConvert.DeserializeObject<List<StateDetails>>(responseContent);
            }

            return View(stateDetails);
        }

        [HttpGet]
        public async Task<IActionResult> GetStates() {
            List<StateDetails> stateDetails = new List<StateDetails>();

            var response = await _httpClient.GetAsync("state/getstates");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                stateDetails = JsonConvert.DeserializeObject<List<StateDetails>>(responseContent);
            }
            return Json(new { data = stateDetails });

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            StateViewModel model = new StateViewModel();

            var countries = await GetCountriesAsync();

            if (countries.Any())
            {
                model.countries = countries;
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(StateViewModel model)
        {

            if (ModelState.IsValid)
            {
                //true

                State state = new State();
                state.StateId = 0;
                state.CountryId = model.CountryId;
                state.Name = model.Name;
                state.Description = model.Description;
                state.StateCode = model.StateCode;

                state.CreatedOn = DateTimeOffset.Now;
                state.CreatedBy = -1;
                state.ModifiedOn = DateTimeOffset.Now;
                state.ModifiedBy = -1;
                state.IsActive = true;

                var response = await SaveStateAsync(state);

                if (response == 1)
                {
                    _notyfService.Success("State created successfully");
                    return RedirectToAction("Index", "State", null);
                }
                else
                {
                    _notyfService.Error("State created un-successfully");
                }

            }
            var countries = await GetCountriesAsync();

            if (countries.Any())
            {
                model.countries = countries;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long StateId)
        {
            StateViewModel model = new StateViewModel();

            var respponse = await GetStateAsync(StateId);

            if (respponse != null)
            {
                model.StateCode = respponse.StateCode;
                model.Name = respponse.Name;
                model.CountryId = respponse.CountryId;
                model.StateId = respponse.StateId;
                model.Description = respponse.Description;
            }

            var countries = await GetCountriesAsync();

            if (countries.Any())
            {
                model.countries = countries;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(StateViewModel model)
        {


            if (ModelState.IsValid)
            {
                State state = new State();
                state.CountryId = model.CountryId;
                state.Name = model.Name;
                state.Description = model.Description;
                state.StateCode = model.StateCode;
                state.CreatedOn = DateTimeOffset.Now;
                state.CreatedBy = -1;
                state.ModifiedOn = DateTimeOffset.Now;
                state.ModifiedBy = -1;
                state.IsActive = true;

                var url = Path.Combine("state/updatestate", model.StateId.ToString());

                var inputState = JsonConvert.SerializeObject(state);

                var requestBody = new StringContent(inputState, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();

                    _notyfService.Success(contentResponse);

                    return RedirectToAction("Index", "State", null);
                }

            }
            var countries = await GetCountriesAsync();

            if (countries.Any())
            {
                model.countries = countries;
            }

            return View(model);
        }

        public async Task<IActionResult> Delete(long StateId)
        {
            var url = Path.Combine("state/deletestate", StateId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                bool isdeleed = JsonConvert.DeserializeObject<bool>(contentResponse);

                if (isdeleed)
                {
                    _notyfService.Success("State deleted successfull");

                    return RedirectToAction("Index", "State", null);
                }
                else
                {
                    _notyfService.Error("Unable to delete State");
                }
            }

            return RedirectToAction("Index", "State", null);
        }
        private async Task<State> GetStateAsync(long stateId)
        {
            State state = new State();

            var url = Path.Combine("state/getstatebyid", stateId.ToString());

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                state = JsonConvert.DeserializeObject<State>(responseContent);
            }
            return state;
        }
        private async Task<List<Country>> GetCountriesAsync()
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

        private async Task<int> SaveStateAsync(State state)
        {
            int isSave = 0;

            var inputState = JsonConvert.SerializeObject(state);

            var requestBody = new StringContent(inputState, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("state/savestate", requestBody);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                isSave = JsonConvert.DeserializeObject<int>(responseContent);

            }
            return isSave;
        }
    }
}
