using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly INotyfService _notyfService;
        List<PaymentMethod> paymentMethods = new List<PaymentMethod>();

        public PaymentMethodController(IPaymentMethodService paymentMethodService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentMethodService = paymentMethodService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View(paymentMethods);
        }
        [HttpGet]
        public async Task<IActionResult> FetchPaymentMethods()
        {
            try
            {
                var response = await _paymentMethodService.GetPaymentMethodsAsync();
                return Json(new { data = paymentMethods });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdatePaymentMethod([FromBody] PaymentMethod paymentMethod)
        {
            try
            {
                await _paymentMethodService.InsertOrUpdatePaymentMethodAsync(paymentMethod);

                _notyfService.Success("PaymentMethod insert or Update Successfully");

                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);

                throw ex;
            }
        }

        public async Task<IActionResult> DeletePaymentmethod(long id)
        {
            try
            {
                var response = await _paymentMethodService.DeletePaymentmethodAsync(id);
                if (response)
                    _notyfService.Success("Paymentmethod deleted successfully");
                else
                    _notyfService.Warning("Paymentmethod deleted un successfully");

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