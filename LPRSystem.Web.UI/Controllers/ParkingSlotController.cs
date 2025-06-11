using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LPRSystem.Web.UI.Controllers
{
    public class ParkingSlotController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyfService;
        public ParkingSlotController(INotyfService notyfService)
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
            List<ParkingSlotDetails> parkingSlotDetails = new List<ParkingSlotDetails>();

            var response = await _httpClient.GetAsync("parkingslot/getparkingslots");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                parkingSlotDetails = JsonConvert.DeserializeObject<List<ParkingSlotDetails>>(responseContent);
            }

            return View(parkingSlotDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ParkingSlotViewModel model = new ParkingSlotViewModel();

            var parkingPlaces = await GetParkingPlacesAsync();

            if (parkingPlaces.Any())
            {
                model.parkingPlaces = parkingPlaces;
            }

            var atms = await GetTMMachinesAsync();

            if (atms.Any())
            {
                model.atmMachines = atms;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingSlotViewModel model)
        {
            //
            if (ModelState.IsValid)
            {
                //true

                ParkingSlot parkingSlot = new ParkingSlot();
                parkingSlot.ParkingSlotId = 0;
                parkingSlot.ParkingSlotCode = model.ParkingSlotCode;
                parkingSlot.ParkingPlaceId = model.ParkingPlaceId;
                parkingSlot.ATMId = model.ATMId;

                parkingSlot.CreatedBy = -1;
                parkingSlot.CreatedOn = DateTimeOffset.Now;
                parkingSlot.ModifiedBy = -1;
                parkingSlot.ModifiedOn = DateTimeOffset.Now;
                parkingSlot.IsActive = true;

                var response = await SaveParkingSlotAsync(parkingSlot);

                if (response == 1)
                {
                    _notyfService.Success("Parkingslot created successfully");
                    return RedirectToAction("Index", "ParkingSlot", null);
                }
                else
                {
                    _notyfService.Error("Parkingslot created un-successfully");
                }

            }
            var parkingPlaces = await GetParkingPlacesAsync();

            if (parkingPlaces.Any())
            {
                model.parkingPlaces = parkingPlaces;
            }

            var atms = await GetTMMachinesAsync();

            if (atms.Any())
            {
                model.atmMachines = atms;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long ParkingSlotId)
        {
            ParkingSlotViewModel model = new ParkingSlotViewModel();

            var respponse = await GetParkingSlotAsync(ParkingSlotId);

            if (respponse != null)
            {
                model.ParkingSlotCode = respponse.ParkingSlotCode;
                model.ATMId = respponse.ATMId;
                model.ParkingPlaceId = respponse.ParkingPlaceId;
                model.ParkingSlotId = respponse.ParkingSlotId;
            }

            var parkingPlaces = await GetParkingPlacesAsync();

            if (parkingPlaces.Any())
            {
                model.parkingPlaces = parkingPlaces;
            }

            var atms = await GetTMMachinesAsync();

            if (atms.Any())
            {
                model.atmMachines = atms;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParkingSlotViewModel model)
        {


            if (ModelState.IsValid)
            {
                ParkingSlot parkingSlot = new ParkingSlot();
                parkingSlot.ParkingSlotId = model.ParkingSlotId;
                parkingSlot.ParkingSlotCode = model.ParkingSlotCode;
                parkingSlot.ParkingPlaceId = model.ParkingPlaceId;
                parkingSlot.ATMId = model.ATMId;

                parkingSlot.CreatedBy = -1;
                parkingSlot.CreatedOn = DateTimeOffset.Now;
                parkingSlot.ModifiedBy = -1;
                parkingSlot.ModifiedOn = DateTimeOffset.Now;
                parkingSlot.IsActive = true;

                var url = Path.Combine("parkingslot/updateparkingslot", model.ParkingSlotId.ToString());

                var inutParkingSlot = JsonConvert.SerializeObject(parkingSlot);

                var requestBody = new StringContent(inutParkingSlot, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();

                    _notyfService.Success(contentResponse);

                    return RedirectToAction("Index", "ParkingSlot", null);
                }

            }
            var parkingPlaces = await GetParkingPlacesAsync();

            if (parkingPlaces.Any())
            {
                model.parkingPlaces = parkingPlaces;
            }

            var atms = await GetTMMachinesAsync();

            if (atms.Any())
            {
                model.atmMachines = atms;
            }
            return View(model);
        }
        public async Task<IActionResult> Delete(long ParkingSlotId)
        {
            var url = Path.Combine("parkingslot/deleteparkingslot", ParkingSlotId.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                bool isdeleed = JsonConvert.DeserializeObject<bool>(contentResponse);

                if (isdeleed)
                {
                    _notyfService.Success("Parkingslot deleted successfull");

                    return RedirectToAction("Index", "ParkingSlot", null);
                }
                else
                {
                    _notyfService.Error("Unable to delete Parkingslot");
                }
            }

            return RedirectToAction("Index", "ParkingSlot", null);
        }
        private async Task<ParkingSlot> GetParkingSlotAsync(long parkingSlotId)
        {
            ParkingSlot parkingSlot = new ParkingSlot();

            var url = Path.Combine("parkingslot/getparkingslotbyid", parkingSlotId.ToString());

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                parkingSlot = JsonConvert.DeserializeObject<ParkingSlot>(responseContent);
            }
            return parkingSlot;
        }
        private async Task<List<ParkingPlace>> GetParkingPlacesAsync()
        {
            List<ParkingPlace> parkingPlaces = new List<ParkingPlace>();

            var response = await _httpClient.GetAsync("parkingplace/getparkingplaces");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                parkingPlaces = JsonConvert.DeserializeObject<List<ParkingPlace>>(responseContent);
            }
            return parkingPlaces;
        }

        private async Task<int> SaveParkingSlotAsync(ParkingSlot parkingSlot)
        {
            int isSave = 0;

            var inputParkingSlot = JsonConvert.SerializeObject(parkingSlot);

            var requestBody = new StringContent(inputParkingSlot, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("parkingslot/saveparkingslot", requestBody);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                isSave = JsonConvert.DeserializeObject<int>(responseContent);

            }
            return isSave;
        }

        private async Task<List<ATMMachine>> GetTMMachinesAsync()
        {
            List<ATMMachine> aTMMachines = new List<ATMMachine>();

            var response = await _httpClient.GetAsync("atmmachine/getatmmachines");

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                aTMMachines = JsonConvert.DeserializeObject<List<ATMMachine>>(responseContent);
            }

            return aTMMachines;
        }
    }
}
