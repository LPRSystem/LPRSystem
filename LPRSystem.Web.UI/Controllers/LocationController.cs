using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly INotyfService _notyfService;
        public LocationController(ILocationService locationService, INotyfService notyfService)
        {
            _locationService = locationService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var response = await _locationService.GetLocationsAsync();
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
