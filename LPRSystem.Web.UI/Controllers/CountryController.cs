using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly INotyfService _notyfService;

        public CountryController(ICountryService countryService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _countryService = countryService;
            _notyfService = notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("Country");

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> FetchAllCountries()
        {
            try
            
            {
                var response = await _countryService.FetchAllCountries();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]

        public async Task<IActionResult> InsertOrUpdateCountry([FromBody] Country country)
        {
            try
            {
                await _countryService.InsertOrUpdateCountry(country);
                _notyfService.Success("Country Insert or update successfully");
                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteCountry(long countryId)
        {
            try
            {
                var response = await _countryService.DeleteCountry(countryId);
                if (response)
                    _notyfService.Success("Country deleted successfully");
                else
                    _notyfService.Warning("Country deleted un successfully");

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
