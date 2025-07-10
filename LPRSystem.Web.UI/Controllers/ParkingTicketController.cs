using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class ParkingTicketController : Controller
    {
        private readonly IParkingTicketService _parkingTicketService;
        private readonly INotyfService _notyfService;

        public ParkingTicketController(IParkingTicketService parkingTicketService,
            INotyfService notyfService)
        {
            _parkingTicketService = parkingTicketService;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchParkingTickets()
        {
            try

            {
                var response = await _parkingTicketService.GetParkingTicketAsync();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> MakePayment(long parkingTicketId)
        {
            return View();
        }

        [HttpGet]
        public async Task <IActionResult> GetTicketDetails(string searchString,long atmId)
        {
            return View(searchString,atmId);
        }

        [HttpGet]
        public async Task<IActionResult> PrintParkingTicket(long parkingTicketId)
        {
            return View("~/Views/ParkingTicket/Print.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> InserOrUpdateParkingTicket([FromBody] ParkingTicket parkingTicket)
        {
            try
            {
                var response = await _parkingTicketService.InserOrUpdateParkingTicketAsync(parkingTicket);

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteParkingTicket(long parkingTicketId)
        {
            try
            {
                var response = await _parkingTicketService.DeleteParkingTicketAsync(parkingTicketId);
                if (response)
                    _notyfService.Success("ParkingTicket deleted successfully");
                else
                    _notyfService.Warning("ParkingTicket deleted un successfully");

                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> FetchParkingTicketById(long parkigTicketId)
        {
            try
            {
                var response = await _parkingTicketService.GetParkingTicketByIdAsync(parkigTicketId);
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
