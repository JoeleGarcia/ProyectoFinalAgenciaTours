using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public string? GetSession()
        {
            return HttpContext.Session.GetString("isSessionActive");
        }

        [HttpGet]
        public IActionResult Register()
        {
            //var sessionActive = GetSession();

            //if (sessionActive != null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistroUsuarioDto registroUsuarioDto)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register", registroUsuarioDto);
                }
                var isRegistered = await _loginService.RegisterAsync(registroUsuarioDto);
                if (isRegistered)
                {
                    //SetSession(registerUserDto.Username);
                    TempData["SuccessMessage"] = "Registro realizado con éxito!.. Por favor Inicie sesion";
                    return RedirectToAction("Index", "Home");
                }

                return View("Register", registroUsuarioDto);
                //return RedirectToAction(nameof(Register) , registerUserDto);
            }
            //catch (NotFoundException ex)
            //{
            //    _logger.LogError(ex, "Error");
            //    ModelState.AddModelError(string.Empty, "Username o password no son válidos.");
            //    return View("Register", registroUsuarioDto);
            //}
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error {Username}", ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                return View("Register", registroUsuarioDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en registrar el usuario {Username}", registroUsuarioDto?.Email);

                TempData["Error"] = ex.Message;
                return RedirectToAction("Error", "Home");
            }

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
            try
            {
                if (string.IsNullOrEmpty(loginUsuarioDto.Email) || string.IsNullOrEmpty(loginUsuarioDto.Password))
                {
                    ModelState.AddModelError(string.Empty, "Username and password are required.");
                    return View("Login");
                }

                var UserData = await _loginService.LoginAsync(loginUsuarioDto.Email, loginUsuarioDto.Password);
                if (UserData is not null)
                {

                    _logger.LogInformation("Usuario {Username} inició sesión correctamente", loginUsuarioDto.Email);

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    TempData["SuccessMessage"] = "Login successful!";

                    return RedirectToAction("Index", "Home");
                }

                _logger.LogWarning("Intento de login fallido para el usuario {Username}", loginUsuarioDto.Email);
                ModelState.AddModelError(string.Empty, "Verificar Username or password");
                TempData["Error"] = "Username o password no son válidos.";

                return View("Login");

            }
            //catch (NotFoundException ex)
            //{
            //    _logger.LogError(ex, "Error");
            //    ModelState.AddModelError(string.Empty, "Username o password no son válidos.");
            //    TempData["Error"] = "Username o password no son válidos.";

            //    return View("Login");
            //}
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error");
                ModelState.AddModelError(string.Empty, "Username o password no son válidos.");
                TempData["Error"] = "Username o password no son válidos.";

                return View("Login");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error en Login para el usuario {Username}", loginUsuarioDto?.Email);

                //TempData["ErrorMessage"] = "Ocurrió un error inesperado. Intente nuevamente.";
                TempData["Error"] = "Ocurrió un error inesperado. Intente nuevamente.";

                return RedirectToAction("Error", "Home");
            }
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
