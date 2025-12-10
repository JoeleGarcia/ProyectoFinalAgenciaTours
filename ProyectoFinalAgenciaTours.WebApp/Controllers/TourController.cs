using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Application.Services;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.WebApp.Models;

namespace ProyectoFinalAgenciaTours.WebApp.Controllers
{
    public class TourController : Controller
    {

        private readonly ILogger<TourController> _logger;
        private readonly IMapper _mapper;
        private readonly ITourService _tourService;
        private readonly IPaisService _paisService;
        private readonly IDestinoService _destinoService;




        public TourController(
                ILogger<TourController> logger
            , IMapper mapper
            , ITourService tourService
            , IPaisService pasiService
            , IDestinoService destinoService



            )
        {
            _logger = logger;
            _mapper = mapper;
            _tourService = tourService;
            _paisService = pasiService;
            _destinoService = destinoService;
        }
        public async Task<IActionResult> Index()
        {

            var _destinos = await _destinoService.GetAllDestinoAsync();

            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");
            ViewData["Destinos"] = new SelectList(
                _destinos.Select(p => new
                {
                    Id = p.Id,
                    NombreCompleto = $"{p.Nombre} ({p.Pais.Nombre})" 
                }).ToList(), 

                "Id",
                "NombreCompleto"
            );

            var _tour = await _tourService.GetAllTourAsync();

            return View(_tour);
        }


        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateTourDto createTourDto)
        {

            var _paises     = await _paisService.GetAllPaisAsync();
            var _destinos   = await _destinoService.GetAllDestinoAsync();

            //ViewData["Paises"] = new SelectList( _paises, "Id", "Nombre");

            var paisesParaSelect = _destinos.Select(p => new 
            {
                Id = p.Id,
                Nombre = $"{p.Nombre} ({p.Pais.Nombre})"
            }).ToList();

            ViewData["Destinos"] = new SelectList( _destinos, "Id", "Nombre");

            if (!ModelState.IsValid)
            {
                return View("Add", createTourDto);
            }

            //var destinoElegido = await _destinoService.GetDestinoByIdAsync(createTourDto.DestinoId);
            //var createTourDtoCompleto = createTourDto with
            //{
            //    Destino = destinoElegido
            //};

            var estaAgregado = await _tourService.AddTourAsync(createTourDto);
            if (estaAgregado is not null)
            {
                return RedirectToAction(nameof(Index));
            }


            return View("Add", createTourDto);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var _pais = await _tourService.GetTourByIdAsync(id);
            var updateTour = _mapper.Map<UpdateTourDto>(_pais);

            var _destinos = await _destinoService.GetAllDestinoAsync();

            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");
            ViewData["Destinos"] = new SelectList(
                _destinos.Select(p => new
                {
                    Id = p.Id,
                    NombreCompleto = $"{p.Nombre} ({p.Pais.Nombre})"
                }).ToList(),

                "Id",
                "NombreCompleto"
            );

            return View(updateTour);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateTourDto updateTourDto)
        {

            var _destinos = await _destinoService.GetAllDestinoAsync();

            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");
            ViewData["Destinos"] = new SelectList(
                _destinos.Select(p => new
                {
                    Id = p.Id,
                    NombreCompleto = $"{p.Nombre} ({p.Pais.Nombre})"
                }).ToList(),

                "Id",
                "NombreCompleto"
            );

            if (!ModelState.IsValid)
            {
                return View("Update", updateTourDto);
            }

            await _tourService.UpdateTourAsync(id, updateTourDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["MateriaError"] = "El identificador no es válido.";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                await _tourService.DeleteTourAsync(id);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, "Error");
                ModelState.AddModelError(string.Empty, "Username o password no son válidos.");
                TempData["MateriaError"] = "El identificador no es válido.";
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Error {Username}", ex.Message);
                ModelState.AddModelError(string.Empty, ex.Message);
                TempData["MateriaError"] = "El identificador no es válido.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al Eliminar Registro {id}", id.ToString());

                ModelState.AddModelError("_addError", ex.Message);
                TempData["MateriaError"] = "El identificador no es válido.";
                return RedirectToAction(nameof(Index));
            }
            SetSuccessToast("Eliminado correctamente", "bg-success");
            TempData["DisplayToast"] = true;

            return RedirectToAction(nameof(Index));
        }

        private void SetSuccessToast(string message, string cssClass)
        {

            TempData["TransactionsToast"] = JsonConvert.SerializeObject(new List<TransactionsToast>
            {
                new TransactionsToast
                {
                    Message = message,
                    CssClass = cssClass
                }
            });
        }
    }
}
