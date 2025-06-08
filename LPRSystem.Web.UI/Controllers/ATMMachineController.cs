using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class ATMMachineController : Controller
    {
        private readonly IATMMachineService _atmMachineservice;
        private readonly INotyfService _notyfService;

        public ATMMachineController(IATMMachineService atmMachineservice, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _atmMachineservice = atmMachineservice;
            _notyfService = notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("ATMMachine");

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> FetchAtmMachine()
        {
            try
            {
                var response = await _atmMachineservice.FetchAllATMMachines();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]

        public async Task<IActionResult> InsertOrUpdateAtmMachine([FromBody] ATMMachine aTMMachine)
        {
            try
            {
                await _atmMachineservice.InsertOrUpdateATMMachine(aTMMachine);
                _notyfService.Success("ATM Machine Insert or update successfully");
                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteATMMachine(long atmId)
        {
            try
            {
                var response = await _atmMachineservice.DeleteATMMachineAsync(atmId);
                if (response)
                    _notyfService.Success("ATM Machine deleted successfully");
                else
                    _notyfService.Warning("ATM Machine deleted un successfully");

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
