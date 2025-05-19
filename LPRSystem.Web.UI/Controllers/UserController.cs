using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using LPRSystem.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LPRSystem.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        List<User> users = new List<User>();
        public UserController(IUserService userService, INotyfService notyfService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _notyfService = notyfService;
            string appUser = httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");



            users.Add(new User
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice.smith@example.com",
                Phone = "123-456-7890",
                RoleId = 1,
                LastPasswordChangedOn = DateTimeOffset.UtcNow.AddDays(-30),
                IsBlocked = false,
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-60),
                ModifiedBy = 1,
                ModifiedOn = DateTimeOffset.UtcNow.AddDays(-30),
                IsActive = true
            });
            users.Add(new User
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                Phone = "098-765-4321",
                RoleId = 2,
                LastPasswordChangedOn = DateTimeOffset.UtcNow.AddDays(-15),
                IsBlocked = false,
                CreatedBy = 1,
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-45),
                ModifiedBy = 1,
                ModifiedOn = DateTimeOffset.UtcNow.AddDays(-15),
                IsActive = true
            });
            users.Add(new User
            {
                Id = 3,
                FirstName = "Charlie",
                LastName = "Brown",
                Email = "charlie.brown@example.com",
                Phone = "555-555-5555",
                RoleId = 3,
                LastPasswordChangedOn = DateTimeOffset.UtcNow.AddDays(-10),
                IsBlocked = true,
                CreatedBy = 2,
                CreatedOn = DateTimeOffset.UtcNow.AddDays(-30),
                ModifiedBy = 2,
                ModifiedOn = DateTimeOffset.UtcNow.AddDays(-10),
                IsActive = false
            });
        }
        public IActionResult Index()
        {
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> FetchUsers()
        {
            try
            {
               // var response = await _userService.FetchAllUser();
                return Json(new { data = users });
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
