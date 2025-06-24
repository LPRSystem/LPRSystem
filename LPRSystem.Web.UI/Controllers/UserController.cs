using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LPRSystem.Web.UI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        
        public UserController(IUserService userService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _notyfService = notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FetchUsers()
        {
            try
            {
                var response = await _userService.FetchAllUser();
                return Json(new { data = response });
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
                _notyfService.Success("User Inserted Or Updated Successfully");
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
