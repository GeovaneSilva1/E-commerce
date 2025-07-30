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
    }
}
