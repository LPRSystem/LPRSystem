using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace LPRSystem.Web.UI.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly INotyfService _notyfService;
        public OrganizationController(INotyfService notyfService)
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
            List<Organization> organization = new List<Organization>();
            var response = await _httpClient.GetAsync("organization/getorganization");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                organization = JsonConvert.DeserializeObject<List<Organization>>(responseContent);
            }

            return View(organization);
        }        

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            OrganizationViewModel model = new OrganizationViewModel();


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganizationViewModel model)
        {

            if (ModelState.IsValid)
            {
                //true

                Organization organization = new Organization();
                organization.Id = 0;
                organization.Name = model.Name;
                organization.Code = model.Code;
                organization.Address = model.Address;
                organization.Email = model.Email;
                organization.Phone = model.Phone;
                organization.CreatedBy = -1;
                organization.CreatedOn = DateTimeOffset.Now;
                organization.ModifiedBy = -1;
                organization.ModifiedOn = DateTimeOffset.Now;
                organization.IsActive = true;

                var response = await SaveOrganizationAsync(organization);

                if (response == 1)
                {
                    _notyfService.Success("Organization created successfully");
                    return RedirectToAction("Index", "Organization", null);
                }
                else
                {
                    _notyfService.Error("Organization created un-successfully");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long Id)
        {
            OrganizationViewModel model = new OrganizationViewModel();

            var respponse = await GetOrganizationAsync(Id);

            if (respponse != null)
            {
                model.Id = respponse.Id;
                model.Name = respponse.Name;
                model.Code = respponse.Code;
                model.Address = respponse.Address;
                model.Email = respponse.Email;
                model.Phone = respponse.Phone;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Organization organization = new Organization();
                organization.Id = model.Id;
                organization.Name = model.Name;
                organization.Code = model.Code;
                organization.Address = model.Address;
                organization.Email = model.Email;
                organization.Phone = model.Phone;
                organization.CreatedBy = -1;
                organization.CreatedOn = DateTimeOffset.Now;
                organization.ModifiedBy = -1;
                organization.ModifiedOn = DateTimeOffset.Now;
                organization.IsActive = true;

                var url = Path.Combine("organization/updateorganization", model.Id.ToString());

                var inputOrganization = JsonConvert.SerializeObject(organization);

                var requestBody = new StringContent(inputOrganization, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(url, requestBody);

                if (response.IsSuccessStatusCode)
                {
                    var contentResponse = await response.Content.ReadAsStringAsync();

                    _notyfService.Success(contentResponse);

                    return RedirectToAction("Index", "Organization", null);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var url = Path.Combine("organization/deleteorganization", Id.ToString());

            var response = await _httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                bool isdeleed = JsonConvert.DeserializeObject<bool>(contentResponse);

                if (isdeleed)
                {
                    _notyfService.Success("Organization deleted successfull");

                    return RedirectToAction("Index", "Organization", null);
                }
                else
                {
                    _notyfService.Error("Unable to delete Organization");
                }
            }
            return RedirectToAction("Index", "Organization", null);
        }
        private async Task<Organization> GetOrganizationAsync(long id)
        {
            Organization organization = new Organization();

            var url = Path.Combine("organization/getorganizationbyid", id.ToString());

            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                organization = JsonConvert.DeserializeObject<Organization>(responseContent);
            }
            return organization;
        }

        private async Task<int> SaveOrganizationAsync(Organization organization)
        {

            int isSave = 0;

            var inputOrganization = JsonConvert.SerializeObject(organization);

            var requestBody = new StringContent(inputOrganization, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("organization/saveorganization", requestBody);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                isSave = JsonConvert.DeserializeObject<int>(responseContent);

            }
            return isSave;
        }
    }
}
