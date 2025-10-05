using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

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
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Elementos obrigatórios não preenchidos");
                }
                var marcaCriada = await _marcaService.AdicionarMarcaAsync(marcaViewModel);
                if (marcaCriada is not null)
                {
                    TempData["MensagemSucesso"] = "Marca criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
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
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos!");
                }

                await _marcaService.AtualizarMarcaAsync(marcaViewModel);
                TempData["MensagemSucesso"] = "Marca atualizada com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeletarMarca(long handle)
        {
            var marca = await _marcaService.ObterMarcaPorIdAsync(handle);
            if (marca is null)
            {
                TempData["MensagemErro"] = "Marca não encontrada!";
                return RedirectToAction(nameof(Index));
            }
            return PartialView("DeletarMarca", marca);
        }

        [HttpPost, ActionName("DeletarMarca")]
        public async Task<IActionResult> DeletarMarcaConfirmada(long Handle)
        {
            try
            {
                await _marcaService.DeletarMarcaAsync(Handle);
                TempData["MensagemSucesso"] = "Marca excluída com sucesso!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
