using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Azure;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;


namespace ProyectoFinalAgenciaTours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILoginService _loginService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, ILoginService loginService)
        {
            _logger = logger;
            _loginService = loginService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [SwaggerOperation(
        Summary = "Autentica al usuario y genera un token JWT.",
        Description = "Verifica las credenciales y, si son válidas, emite un Access Token Bearer. Este endpoint es público y no requiere autenticación previa."
         )]
        [SwaggerResponse(
        StatusCodes.Status200OK,
        "Inicio de sesión exitoso.",
        typeof(LoginJwtResponseDto)
        )]
        [SwaggerResponse(
        StatusCodes.Status400BadRequest,
        "Credenciales inválidas, errores de validación de entrada o fallos de negocio."
         )]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto loginUserDto)
        {
            try
            {
                var response = await _loginService.LoginForJwtAsync(loginUserDto);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Message = ex.Message, Errors = ex.TargetSite });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado al procesar la solicitud.");
            }
        }


        [HttpPost("register")]
        [AllowAnonymous]
        [SwaggerOperation(
            Summary = "Registra un nuevo usuario en el sistema.",
            Description = "Crea una nueva cuenta de usuario con credenciales y datos básicos. No requiere autenticacion previa."
        )]
        [SwaggerResponse(
        StatusCodes.Status200OK,
            "Registro completado exitosamente. El usuario puede iniciar sesion.",
            typeof(RegistroUsuarioDto)
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.BadRequest,
            "Fallo en la validacion (ej. contraseña débil) o el email/username ya está en uso."
        )]
        public async Task<IActionResult> Register([FromBody] RegistroUsuarioDto registerUserDto)
        {
            try
            {
                var response = await _loginService.RegisterAsync(registerUserDto);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Message = ex.Message, Errors = ex.TargetSite });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado al procesar la solicitud.");
            }
        }

        [HttpGet("me")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Obtiene la informacion del usuario actualmente autenticado.",
            Description = "Utiliza el token JWT provisto en el encabezado 'Authorization' para identificar y devolver los datos del usuario logueado."
        )]
        [SwaggerResponse(
        StatusCodes.Status200OK,
            "Información del usuario devuelta con éxito.",
            typeof(UsuarioDto)
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.Unauthorized,
            "No autenticado. Token JWT ausente o inválido."
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.BadRequest,
            "Error al procesar la solicitud."
        )]
        public async Task<IActionResult> UserInfo()
        {

            try
            {
                var _userId = _loginService.GetUserId();
                var _userInfo = await _loginService.UserInfo(_userId);
                return Ok(_userInfo);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { Message = ex.Message, Errors = ex.TargetSite });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado al procesar la solicitud.");
            }

        }

    }
}
