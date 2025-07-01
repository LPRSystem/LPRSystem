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

        public ParkingTicketController(IParkingTicketService parkingTicketService, INotyfService notyfService)
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
        [HttpPost]
        public async Task<IActionResult> InserOrUpdateParkingTicket(ParkingTicket parkingTicket)
        {
            try
            {
                await _parkingTicketService.InserOrUpdateParkingTicketAsync(parkingTicket);
                _notyfService.Success("ParkingTicket insertOrUpdate Successfully.");
                return Json(new { data = true });
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
    }    }
