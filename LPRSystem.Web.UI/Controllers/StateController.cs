using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    [Authorize]
    public class StateController : Controller
    {
        private readonly IStateService _stateService;
        private readonly INotyfService _notyfService;
        public StateController(IStateService stateService, INotyfService notyfService)
        {
            _stateService = stateService;
            _notyfService = notyfService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
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
                if (state.StateId == 0 || state.StateId==null)
                    await _stateService.InsertOrUpdateStateAsync(state);
                else
                    await _stateService.UpdateStateAsync(state);

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
