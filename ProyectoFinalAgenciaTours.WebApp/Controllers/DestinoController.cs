using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Application.Services;
using ProyectoFinalAgenciaTours.WebApp.Models;

namespace ProyectoFinalAgenciaTours.WebApp.Controllers
{
    [Authorize]
    public class DestinoController : Controller
    {
        private readonly ILogger<DestinoController> _logger;
        private readonly IMapper _mapper;
        private readonly IDestinoService _destinoService;
        private readonly IPaisService _paisService;



        public DestinoController(
                ILogger<DestinoController> logger
            , IMapper mapper
            , IDestinoService destinoService
            , IPaisService paisService

            )
        {
            _logger = logger;
            _mapper = mapper;
            _destinoService = destinoService;
            _paisService = paisService;
        }

        public async Task<IActionResult> Index()
        {
            var _destino = await _destinoService.GetAllDestinoAsync();

            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");

            return View(_destino);
        }

        [HttpGet]
        public IActionResult Add()
        {
            //return PartialView("_Add");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateDestinoDto createDestinoDto)
        {
            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");

            if (!ModelState.IsValid)
            {
                return View("Add", createDestinoDto);
            }


            var estaAgregado = await _destinoService.AddDestinoAsync(createDestinoDto);
            if (estaAgregado is not null)
            {
                return RedirectToAction(nameof(Index));
            }
            

            return View("Add", createDestinoDto);
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
                await _destinoService.DeleteDestinoAsync(id);
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var _destino = await _destinoService.GetDestinoByIdAsync(id);
            var updateDestino = _mapper.Map<UpdateDestinoDto>(_destino);
            
            ViewData["Paises"] = new SelectList(await _paisService.GetAllPaisAsync(), "Id", "Nombre");

            return View(updateDestino);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateDestinoDto updateDestinoDto)
        {

            if (!ModelState.IsValid)
            {
                return View("Update", updateDestinoDto);
            }

            await _destinoService.UpdateDestinoAsync(id, updateDestinoDto);

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
