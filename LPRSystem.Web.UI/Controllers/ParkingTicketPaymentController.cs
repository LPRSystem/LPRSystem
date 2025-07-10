using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.UI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class ParkingTicketPaymentController : Controller
    {
        private readonly IParkingTicketPaymentService _paymentService;
        private readonly INotyfService _notyfService;

        public ParkingTicketPaymentController(IParkingTicketPaymentService paymentService,
            INotyfService notyfService)
        {
            _paymentService = paymentService;
            _notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> InsertParkingTicketPayment([FromBody] ParkingTicketPayment parkingTicketPayment)
        {
            try
            {
                var response = await _paymentService.InsertParkingTicketPaymentAsync(parkingTicketPayment);

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchParkingTicketPaymentById(long parkingTicketPaymentId)
        {
            try

            {
                var response = await _paymentService.GetParkingTicketPaymentByIdAsync(parkingTicketPaymentId);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchParkingTicketPayments()
        {
            try

            {
                var response = await _paymentService.GetParkingTicketPaymentAsync();
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
