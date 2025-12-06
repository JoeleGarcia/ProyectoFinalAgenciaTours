using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.WebApp.Models;

namespace ProyectoFinalAgenciaTours.WebApp.Controllers
{
    public class PaisController : Controller
    {
        private readonly ILogger<PaisController> _logger;
        private readonly IMapper _mapper;
        private readonly IPaisService _paisService;


        public PaisController(
                ILogger<PaisController> logger
            ,   IMapper mapper
            ,   IPaisService paisService
            )
        {
            _logger         = logger;
            _mapper         = mapper;
            _paisService    = paisService;
        }

        public async Task<IActionResult> Index()
        {
            var _pais = await _paisService.GetAllPaisAsync();
            return View(_pais);
        }

        public IActionResult Add()
        {
            //return PartialView("_Add");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int id, CreatePaisDto createPaisDto)
        {

            if (!ModelState.IsValid)
            {
                return View("Add", createPaisDto);
            }

            var estaAgregado = await _paisService.AddPaisAsync(createPaisDto);
            if (estaAgregado is not null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View("Add", createPaisDto);
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
                await _paisService.DeletePaisAsync(id);
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
            var _pais = await _paisService.GetPaisByIdAsync(id);
            var updateMateria = _mapper.Map<UpdatePaisDto>(_pais);

            return View(updateMateria);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdatePaisDto updatePaisDto)
        {

            if (!ModelState.IsValid)
            {
                return View("Update", updatePaisDto);
            }

            await _paisService.UpdatePaisAsync(id, updatePaisDto);

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
