using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class ATMMachineController : Controller
    {
        private readonly IATMMachineService _atmMachineservice;
        private readonly INotyfService _notyfService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
