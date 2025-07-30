using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class MarcasController : Controller
    {
        private readonly IMarcaService _marcaService;

        public MarcasController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaViewModel>>> Index()
        {
            var marcas = await _marcaService.ObterMarcasAsync();
            return View(marcas);
        }

        [HttpGet]
        public async Task<IActionResult> CriarMarca()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var marcaCriada = await _marcaService.AdicionarMarcaAsync(marcaViewModel);
                if (marcaCriada is not null)
                {
                    TempData["MensagemSucesso"] = "Marca criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(marcaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarMarca(long handle)
        {
            var marca = await _marcaService.ObterMarcaPorIdAsync(handle);
            if (marca is null)
            {
                TempData["MensagemErro"] = "Marca não encontrada!";
                return RedirectToAction(nameof(Index));
            }

            return PartialView("AtualizarMarca", marca);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarMarca(MarcaViewModel marcaViewModel)
        {
            if (ModelState.IsValid)
            {
                var marcaAtualizada = await _marcaService.AtualizarMarcaAsync(marcaViewModel);
                if (marcaAtualizada is not null)
                {
                    TempData["MensagemSucesso"] = "Marca atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(marcaViewModel);
        }
    }
}
