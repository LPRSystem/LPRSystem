using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly INotyfService _notyfService;

        public CityController(ICityService cityService, INotyfService notyfService)
        {
           _cityService = cityService;
           _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCity()

        {
            try
            {
                var response = await _cityService.GetCityAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]

        public async Task<IActionResult> InsertOrUpdateCity([FromBody] City city)
        {
            try
            {
                await _cityService.InsertOrUpdateCityAsync(city);
                _notyfService.Success("City Insert or update successfully");
                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        public async Task<IActionResult> DeleteCity(long cityid)
        {
            try
            {
                var response = await _cityService.DeleteCityAsync(cityid);
                if (response)
                    _notyfService.Success("City deleted successfully");
                else
                    _notyfService.Warning("City deleted un successfully");

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
