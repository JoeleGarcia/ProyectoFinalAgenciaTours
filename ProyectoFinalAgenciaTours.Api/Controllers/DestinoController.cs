using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class DestinoController : ControllerBase
    {

        private readonly IDestinoService _destinoService;
        private readonly IMapper _mapper;
        private readonly ILogger<DestinoController> _logger;

        public DestinoController(ILogger<DestinoController> logger, IDestinoService destinoService, IMapper mapper)
        {
            _logger = logger;
            _destinoService = destinoService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
       Summary = "Listado de destinos",
       Description = "Muestra el listado de todos los destinos registrados"
        )]
        [SwaggerResponse(
       StatusCodes.Status200OK,
       "Listado de destinos",
       typeof(IEnumerable<DestinoDto>)
       )]
        [SwaggerResponse(
        StatusCodes.Status400BadRequest,
       "No existen destinos registrados"
        )]
        public async Task<IActionResult> GetAlldestinos()
        {
            try
            {
                var destino = await _destinoService.GetAllDestinoAsync();
                return Ok(destino);
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
            Summary = "Crea una nuevo destino en el sistema.",
            Description = "Registra un destino"
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.NoContent,
            "destino creado exitosamente."
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.BadRequest,
            "Fallo en la validacion de la entrada o una regla de negocio fue violada (ej. duplicidad, destino ya existe)"
        )]
        public async Task<IActionResult> Add(CreateDestinoDto createDestinoDto)
        {
            try
            {
                var destinoAgregada = await _destinoService.AddDestinoAsync(createDestinoDto);
                return Ok(destinoAgregada);
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
          Summary = "Actualiza completamente la informacion de un destino existente.",
          Description = "Reemplaza todos los datos del destino."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NoContent,
          "Actualizacion completada con éxito."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NotFound,
          "El ID del destino no existe en el sistema."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.BadRequest,
          "Datos de entrada invalidos o fallo de negocio."
      )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDestinoDto updateDestinoDto)
        {
            if (id <= 0)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
            }

            try
            {
                await _destinoService.UpdateDestinoAsync(id, updateDestinoDto);
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
                Summary = "Elimina una destino permanentemente.",
                Description = "Solo requiere el ID de la destino en la ruta."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NoContent,
                "destino eliminado exitosamente."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NotFound,
                "La destino con el ID especificado no fue encontrado."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.BadRequest,
                "Fallo en la operacion debido a reglas de negocio (ej. la destino tiene calificaciones activas)."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id no esta en el formato correcto");
            }


            try
            {
                await _destinoService.DeleteDestinoAsync(id);
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
