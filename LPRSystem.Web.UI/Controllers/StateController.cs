using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using LPRSystem.Web.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    [Authorize]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly INotyfService _notyfService;
        public StateController(IStateService stateService,INotyfService notyfService)
        {
            _stateService = stateService;
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
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(State state)
        {
            try
            {
                if (state == null)
                {
                    _notyfService.Error("Something is went wrong please try again");
                    return View(state);
                }
                state.StateId = 0;

                state.CreatedOn = DateTime.Now;
                state.CreatedBy = -1;
                state.ModifiedOn = DateTime.Now;
                state.ModifiedBy = -1;
                state.IsActive = true;

                var response = await _stateService.InsertOrUpdateStateAsync(state);

                _notyfService.Success("State insert or Update Successfully");

                return RedirectToAction("Index", "State", null);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long stateId)
        {
            try
            {
                var response = await _stateService.GetStateByIdAsync(stateId);
                return View(response);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(State state)
        {
            try
            {
                state.CreatedOn = DateTime.Now;
                state.CreatedBy = -1;
                state.ModifiedOn = DateTime.Now;
                state.ModifiedBy = -1;
                state.IsActive = true;

                var response = await _stateService.UpdateStateAsync(state);
                if (response != null)
                {
                    _notyfService.Success("State  Update Successfully");

                    return RedirectToAction("Index", "State", null);
                }

                return View(state);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchStates()
        {
            try
            {
                var response = await _stateService.GetStatesAync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateStateAsync([FromBody] State state)
        {
            try
            {
                if (state.StateId > 0)
                    await _stateService.InsertOrUpdateStateAsync(state);
                else
                    await _stateService.InsertOrUpdateStateAsync(state);

                _notyfService.Success("State insert or Update Successfully");

                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);

                throw ex;
            }
        }

        public async Task<IActionResult> DeleteStateAsync(long stateId)
        {
            try
            {
                var response = await _stateService.DeleteStateAsync(stateId);
                if (response)
                    _notyfService.Success("State deleted successfully");
                else
                    _notyfService.Warning("State deleted un successfully");

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
