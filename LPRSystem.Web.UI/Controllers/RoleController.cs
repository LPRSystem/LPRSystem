using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.UI.Repository;
using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LPRSystem.Web.UI.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly INotyfService _notyfService;
        List<Role> roles = new List<Role>();

        public RoleController(IRoleService roleService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _notyfService = notyfService;
            _roleService = roleService;
           
        }
        public IActionResult Index()
        {
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> FetchRoles()
        {
            try
            {
                var response = await _roleService.GetRolesAsync();
                return Json(new { data = roles });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateRole([FromBody] RoleManagement role)
        {
            try
            {
                await _roleService.InsertOrUpdateRole(role);
                _notyfService.Success("Insert/update role successfully");
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
