using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LojaVirtual.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;

        public ProdutosController(IProdutoService produtoService, 
                                  ICategoriaService categoriaService, 
                                  IMarcaService marcaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            var result = await _produtoService.ObterProdutosAsync();
            
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> CriarProduto()
        {
            ViewBag.CategoriaId = new SelectList(await _categoriaService.ObterCategoriasAsync(), "Handle", "Nome");
            ViewBag.MarcaId = new SelectList(await _marcaService.ObterMarcasAsync(), "Handle", "Nome");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CriarProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            { 
                var produtoCriado = await _produtoService.CriarProdutoAsync(produtoViewModel);
                if (produtoCriado is not null)
                {
                    TempData["MensagemSucesso"] = "Produto criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
            }
            
            ViewBag.CategoriaId = new SelectList(await _categoriaService.ObterCategoriasAsync(), "Handle", "Nome");
            ViewBag.MarcaId = new SelectList(await _marcaService.ObterMarcasAsync(), "Handle", "Nome");
            return View(produtoViewModel);
        }

    }
}
