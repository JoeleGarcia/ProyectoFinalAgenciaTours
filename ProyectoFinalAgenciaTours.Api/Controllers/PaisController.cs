using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ProyectoFinalAgenciaTours.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {


        private readonly IPaisService _paisService;
        private readonly IMapper _mapper;
        private readonly ILogger<PaisController> _logger;

        public PaisController(ILogger<PaisController> logger, IPaisService paisService, IMapper mapper)
        {
            _logger = logger;
            _paisService = paisService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
       Summary = "Listado de paises",
       Description = "Muestra el listado de todos los paises registrados"
        )]
        [SwaggerResponse(
       StatusCodes.Status200OK,
       "Listado de paises",
       typeof(IEnumerable<PaisDto>)
       )]
        [SwaggerResponse(
        StatusCodes.Status400BadRequest,
       "No existen paises registrados"
        )]
        public async Task<IActionResult> GetAllpaises()
        {
            try
            {
                var paises = await _paisService.GetAllPaisAsync();
                return Ok(paises);
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


        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Crea una nuevo pais en el sistema.",
            Description = "Registra un pais"
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.NoContent,
            "Pais creado exitosamente."
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.BadRequest,
            "Fallo en la validacion de la entrada o una regla de negocio fue violada (ej. duplicidad, pais ya existe)"
        )]
        public async Task<IActionResult> Add(CreatePaisDto createPaisDto)
        {
            try
            {
                var paisAgregada = await _paisService.AddPaisAsync(createPaisDto);
                return Ok(paisAgregada);
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

        [HttpPut("Update/{id:int}")]
        [Authorize]
        [SwaggerOperation(
          Summary = "Actualiza completamente la informacion de un pais existente.",
          Description = "Reemplaza todos los datos del pais."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NoContent,
          "Actualizacion completada con éxito."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NotFound,
          "El ID del pais no existe en el sistema."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.BadRequest,
          "Datos de entrada invalidos o fallo de negocio."
      )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePaisDto updatePaisDto)
        {
            if (id <= 0)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
            }

            try
            {
                await _paisService.UpdatePaisAsync(id, updatePaisDto);
                return NoContent();
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

        [HttpDelete("Delete/{id:int}")]
        [Authorize]
        [SwaggerOperation(
                Summary = "Elimina una pais permanentemente.",
                Description = "Solo requiere el ID de la pais en la ruta."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NoContent,
                "pais eliminado exitosamente."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NotFound,
                "La pais con el ID especificado no fue encontrado."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.BadRequest,
                "Fallo en la operacion debido a reglas de negocio (ej. la pais tiene calificaciones activas)."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id no esta en el formato correcto");
            }


            try
            {
                await _paisService.DeletePaisAsync(id);
                return NoContent();
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
