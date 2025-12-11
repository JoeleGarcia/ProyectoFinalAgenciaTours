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
    public class TourController : ControllerBase
    {

        private readonly ITourService _tourService;
        private readonly IMapper _mapper;
        private readonly ILogger<TourController> _logger;

        public TourController(ILogger<TourController> logger, ITourService tourService, IMapper mapper)
        {
            _logger = logger;
            _tourService = tourService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [SwaggerOperation(
       Summary = "Listado de tours",
       Description = "Muestra el listado de todos los tours registrados"
        )]
        [SwaggerResponse(
       StatusCodes.Status200OK,
       "Listado de tours",
       typeof(IEnumerable<TourDetailDto>)
       )]
        [SwaggerResponse(
        StatusCodes.Status400BadRequest,
       "No existen tours registrados"
        )]
        public async Task<IActionResult> GetAlltours()
        {
            try
            {
                var tour = await _tourService.GetAllTourAsync();
                return Ok(tour);
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
            Summary = "Crea una nuevo tour en el sistema.",
            Description = "Registra un tour"
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.NoContent,
            "tour creado exitosamente."
        )]
        [SwaggerResponse(
            (int)HttpStatusCode.BadRequest,
            "Fallo en la validacion de la entrada o una regla de negocio fue violada (ej. duplicidad, tour ya existe)"
        )]
        public async Task<IActionResult> Add(CreateTourDto createTourDto)
        {
            try
            {
                var tourAgregada = await _tourService.AddTourAsync(createTourDto);
                return Ok(tourAgregada);
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
          Summary = "Actualiza completamente la informacion de un tour existente.",
          Description = "Reemplaza todos los datos del tour."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NoContent,
          "Actualizacion completada con éxito."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.NotFound,
          "El ID del tour no existe en el sistema."
      )]
        [SwaggerResponse(
          (int)HttpStatusCode.BadRequest,
          "Datos de entrada invalidos o fallo de negocio."
      )]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTourDto updateTourDto)
        {
            if (id <= 0)
            {
                return BadRequest("El ID de la ruta no coincide con el ID del cuerpo de la solicitud.");
            }

            try
            {
                await _tourService.UpdateTourAsync(id, updateTourDto);
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
                Summary = "Elimina una tour permanentemente.",
                Description = "Solo requiere el ID de la tour en la ruta."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NoContent,
                "tour eliminado exitosamente."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.NotFound,
                "La tour con el ID especificado no fue encontrado."
            )]
        [SwaggerResponse(
                (int)HttpStatusCode.BadRequest,
                "Fallo en la operacion debido a reglas de negocio (ej. la tour tiene calificaciones activas)."
            )]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id no esta en el formato correcto");
            }


            try
            {
                await _tourService.DeleteTourAsync(id);
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
