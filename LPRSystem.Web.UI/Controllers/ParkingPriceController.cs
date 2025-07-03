using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Controllers
{
    public class ParkingPriceController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyfService;
        private readonly IParkingPriceService _parkingPriceService;

        public ParkingPriceController(INotyfService notyfService,
            IParkingPriceService parkingPriceService)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:7239/api/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.Timeout = new TimeSpan(0, 0, 120);

            _notyfService = notyfService;
            _parkingPriceService = parkingPriceService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<ParkingPrice> parkingPrice = new List<ParkingPrice>();

            parkingPrice = await _parkingPriceService.GetParkingPriceListAsync();

            return View(parkingPrice);

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ParkingPriceViewModel model = new ParkingPriceViewModel();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingPriceViewModel model)
        {

            if (ModelState.IsValid)
            {
                //true

                ParkingPrice parkingPrice = new ParkingPrice();
                parkingPrice.ParkingPriceId = 0;
                parkingPrice.Duration = model.Duration;
                parkingPrice.Price = model.Price;
                parkingPrice.CreatedBy = -1;
                parkingPrice.CreatedOn = DateTimeOffset.Now;
                parkingPrice.ModifiedBy = -1;
                parkingPrice.ModifiedOn = DateTimeOffset.Now;
                parkingPrice.IsActive = true;

                var response = await SaveParkingPriceAsync(parkingPrice);

                if (response == 1)
                {
                    _notyfService.Success("ParkingPrice created successfully");
                    return RedirectToAction("Index", "ParkingPrice", null);
                }
                else
                {
                    _notyfService.Error("ParkingPrice created un-successfully");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long ParkingPriceId)
        {
            ParkingPriceViewModel model = new ParkingPriceViewModel();

            var response = await GetParkingPriceAsync(ParkingPriceId);

            if (response != null)
            {
                model.ParkingPriceId = response.ParkingPriceId;
                model.Duration = response.Duration;
                model.Price = response.Price;

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParkingPriceViewModel model)
        {
            if (ModelState.IsValid)
            {
                ParkingPrice parkingPrice = new ParkingPrice();
                parkingPrice.ParkingPriceId = model.ParkingPriceId;
                parkingPrice.Duration = model.Duration;
                parkingPrice.Price = model.Price;
                parkingPrice.CreatedBy = -1;
                parkingPrice.CreatedOn = DateTimeOffset.Now;
                parkingPrice.ModifiedBy = -1;
                parkingPrice.ModifiedOn = DateTimeOffset.Now;
                parkingPrice.IsActive = true;

                var url = Path.Combine("parkingprice/updateparkingprice", model.ParkingPriceId.ToString());

                var inputParkingPrice = JsonConvert.SerializeObject(parkingPrice);

                var requestBody = new StringContent(inputParkingPrice, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();

                    _notyfService.Success(contentResponse);

                    return RedirectToAction("Index", "ParkingPrice", null);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(long ParkingPriceId)
        {
            var url = Path.Combine("parkingprice/deleteparkingprice", ParkingPriceId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                bool isdeleted = JsonConvert.DeserializeObject<bool>(contentResponse);

                if (isdeleted)
                {
                    _notyfService.Success("ParkingPrice deleted successfully");

                    return RedirectToAction("Index", "ParkingPrice", null);
                }
                else
                {
                    _notyfService.Error("Unable to delete ParkingPrice");
                }
            }
            return RedirectToAction("Index", "ParkingPrice", null);
        }


      
        private async Task<ParkingPrice> GetParkingPriceAsync(long parkingPriceId)
        {
            ParkingPrice parkingPrice = new ParkingPrice();

            var url = Path.Combine("parkingprice/getparkingpricebyid", parkingPriceId.ToString());

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                parkingPrice = JsonConvert.DeserializeObject<ParkingPrice>(responseContent);
            }
            return parkingPrice;
        }



        private async Task<int> SaveParkingPriceAsync(ParkingPrice parkingPrice)
        {

            int isSave = 0;

            var inputParkingPrice = JsonConvert.SerializeObject(parkingPrice);

            var requestBody = new StringContent(inputParkingPrice, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("parkingprice/saveparkingprice", requestBody);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                isSave = JsonConvert.DeserializeObject<int>(responseContent);

            }
            return isSave;
        }

        [HttpGet]
        public async Task<IActionResult> GetParkingPrices()
        {
            try
            {
                var parkingPriceList = await _parkingPriceService.GetParkingPriceListAsync();

                return Json(new { data = parkingPriceList });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}

