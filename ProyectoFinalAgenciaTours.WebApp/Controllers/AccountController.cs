using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Interfaces;

namespace ProyectoFinalAgenciaTours.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly ILoginService _loginService;

        public AccountController(ILogger<AccountController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string? GetSession()
        {
            return HttpContext.Session.GetString("isSessionActive");
        }


        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var sessionActive = GetSession();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/");
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDto loginUsuarioDto, string? returnUrl = null)
        {
           
        }


        [HttpPost]
        public IActionResult Logout()
        {

            _loginService.LogoutAsync();

            var sessionActive = GetSession();

            if (sessionActive != null)
            {
                TempData["SuccessMessage"] = "You have been logged out successfully.";
            }

            return RedirectToAction("Index", "Account");
        }
    }
}
