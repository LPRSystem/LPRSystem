using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly IPaymentMethodService _paymentMethodService;
        private readonly INotyfService _notyfService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService, INotyfService notyfService)

        {
            _paymentMethodService = paymentMethodService;
            _notyfService = notyfService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _paymentMethodService.GetPaymentMethodsAsync();
                return View(response);
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PaymentMethod paymentMethod)
        {
            try
            {
                if (paymentMethod == null)
                {
                    _notyfService.Error("Somthing is wrong please try again");
                    return View(paymentMethod);
                }
                paymentMethod.Id = 0;
                paymentMethod.CreatedBy = -1;

                paymentMethod.ModifiedBy = -1;
                paymentMethod.CreatedOn = DateTime.Now;
                paymentMethod.ModifiedOn = DateTime.Now;
                paymentMethod.IsActive = true;


                var response = await _paymentMethodService.InsertOrUpdatePaymentMethodAsync(paymentMethod);
                _notyfService.Success("PaymentMethod insert or Update Successfully");

                return RedirectToAction("Index", "PaymentMethod", null);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var response = await _paymentMethodService.GetPaymentMethodByIdAsync(id);
                return View(response);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PaymentMethod paymentMethod)
        {
            try
            {

                paymentMethod.CreatedBy = -1;
                paymentMethod.ModifiedBy = -1;
                paymentMethod.CreatedOn = DateTime.Now;
                paymentMethod.ModifiedOn = DateTime.Now;
                paymentMethod.IsActive = true;


                var response = await _paymentMethodService.UpdatePaymentMethodAsync(paymentMethod);
                if (response != null)
                {
                    _notyfService.Success("PaymentMethod  Update Successfully");

                    return RedirectToAction("Index", "PaymentMethod", null);
                }

                return View(paymentMethod);

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchPaymentMethods()
        {
            try
            {
                var response = await _paymentMethodService.GetPaymentMethodsAsync();
                return Json(new { data = response });
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