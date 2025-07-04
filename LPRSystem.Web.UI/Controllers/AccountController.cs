using AspNetCoreHero.ToastNotification.Abstractions;
using LPRSystem.Web.UI.Factory;
using LPRSystem.Web.UI.Interfaces;
using LPRSystem.Web.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace LPRSystem.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        public readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IATMMachineService _atmService;
        public AccountController(IUserService userService,
            INotyfService notyfService,
            IHttpContextAccessor contextAccessor,
            IATMMachineService atmService)
        {
            _userService = userService;
            _notyfService = notyfService;
            _contextAccessor = contextAccessor;
            _atmService = atmService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Authenticate authenticate)
        {
            var response = await _userService.AuthenticationAsync(authenticate);


            var atmResponse = new ATMMachinesData();


            if (response != null)
            {
                if (response.Status)
                {
                    if (response.RoleId == 15)
                    {
                        var atms = await _atmService.FetchAllATMMachines();

                        if (atms.Any())
                        {
                            atmResponse = atms.Where(x => x.ATMCode == response.FirstName).FirstOrDefault();
                        }
                    }


                    _contextAccessor.HttpContext.Session.SetString("ApplicationUser", JsonConvert.SerializeObject(response));

                    var claimsIdentity = UserPrincipal.GenarateUserPrincipal(response);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                                                              new ClaimsPrincipal(claimsIdentity),
                                                                              new AuthenticationProperties
                                                                              {
                                                                                  IsPersistent = true,
                                                                                  ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                                                                              });

                }
                else
                {
                    _notyfService.Error(response.Message);
                }
            }

            return Json(new { data = response, atm = atmResponse });
        }

        public async Task<IActionResult> Logout()
        {
            //var appuser = _httpContextAccessor.HttpContext.Session.GetString("ApplicationUser");
            //HttpContext.Session.Remove("AccessToken");
            //HttpContext.Session.Remove("ApplicationUser");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete("allowCookies");
            return RedirectToAction("Login", "Account", null);
        }
    }
}
