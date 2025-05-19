using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LPRSystem.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        private readonly ApplicationUser _applicattionUser;
        public UserController(IUserService userService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor) 
        {
            _userService = userService;
            _notyfService= notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            _applicattionUser = JsonConvert.DeserializeObject<ApplicationUser>(appUser);
        }
        public IActionResult Index()
        {
            return View("~/Views/User/Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> FetchUsers()
        {
            try
            {
                var response = await _userService.FetchAllUser();
                return Json(new {data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateUser([FromBody] UserRegistration registration)
        {
            try
            {
                await _userService.InsertOrUpdateUser(registration);
                _notyfService.Success("User Created Successfully");
                return Json(new { data = true });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
    }
}
