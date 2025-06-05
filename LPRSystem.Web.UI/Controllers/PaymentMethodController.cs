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
        List<PaymentMethod> paymentMethods= new List<PaymentMethod>();

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
    }
}
