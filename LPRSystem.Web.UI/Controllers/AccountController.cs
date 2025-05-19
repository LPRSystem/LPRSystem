using LPRSystem.Web.UI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        public readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var users = await _userService.FetchAllUser();
            return View();
        }
    }
}
