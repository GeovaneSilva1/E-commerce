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
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
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
            if (ModelState.IsValid)
            {
                var categoriaCriada = await _categoriaService.AdicionarCategoriaAsync(categoriaViewModel);
                if (categoriaCriada is not null)
                {
                    TempData["MensagemSucesso"] = "Categoria criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
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
            if (ModelState.IsValid)
            {
                var categoriaAtualizada = await _categoriaService.AtualizarCategoriaAsync(categoriaViewModel);
                if (categoriaAtualizada is not null)
                {
                    TempData["MensagemSucesso"] = "Categoria atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(categoriaViewModel);
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
            var categoriaExcluida = await _categoriaService.DeletarCategoriaAsync(Handle);
            
            if (categoriaExcluida)
            {
                TempData["MensagemSucesso"] = "Categoria excluída com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            TempData["MensagemErro"] = "Erro ao deletar categoria!";
            return RedirectToAction(nameof(Index));
        }
    }
}
