using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
        {
            var categorias = await _categoriaService.ObterCategoriasAsync();

            return View(categorias);
        }

        [HttpGet]
        public async Task<IActionResult> CriarCategoria()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria(CategoriaViewModel categoriaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Elementos obrigatórios não preenchidos");
                }

                var categoriaCriada = await _categoriaService.AdicionarCategoriaAsync(categoriaViewModel);
                if (categoriaCriada is not null)
                {
                    TempData["MensagemSucesso"] = "Categoria criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }
            return View(categoriaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AtualizarCategoria(long Handle)
        {
            var categoria = await _categoriaService.ObterCategoriaPorIdAsync(Handle);
            return PartialView("AtualizarCategoria", categoria);
        }
        [HttpPost]
        public async Task<IActionResult> AtualizarCategoria(CategoriaViewModel categoriaViewModel)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("Dados inválidos!");
                }

                await _categoriaService.AtualizarCategoriaAsync(categoriaViewModel);
                TempData["MensagemSucesso"] = "Categoria atualizada com sucesso!";

            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
            //return View(categoriaViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarCategoria(long Handle)
        {
            var categoria = await _categoriaService.ObterCategoriaPorIdAsync(Handle);
            if (categoria is null)
            {
                TempData["MensagemErro"] = "Categoria não encontrada!";
                return RedirectToAction(nameof(Index));
            }

            return PartialView("DeletarCategoria", categoria);
        }

        [HttpPost(), ActionName("DeletarCategoria")]
        public async Task<IActionResult> DeletarCategoriaConfirmada(long Handle)
        {
            try
            {
                await _categoriaService.DeletarCategoriaAsync(Handle);
                TempData["MensagemSucesso"] = "Categoria excluída com sucesso!";

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
