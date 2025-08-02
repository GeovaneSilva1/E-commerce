using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [HttpGet]
        public async Task<IActionResult> AtualizarProduto(long Handle)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(Handle);

            if (produto is null)
            {
                TempData["MensagemErro"] = "Produto não encontrado!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(await _categoriaService.ObterCategoriasAsync(), "Handle", "Nome");
            ViewBag.MarcaId = new SelectList(await _marcaService.ObterMarcasAsync(), "Handle", "Nome");

            return PartialView("AtualizarProduto", produto);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarProduto(ProdutoViewModel produtoViewModel)
        {
            try
            {
                var produtoAtualizado = await _produtoService.AtualizarProdutoAsync(produtoViewModel);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DeletarProduto(long Handle)
        {
            try
            {
                var produto = await _produtoService.ObterProdutoPorIdAsync(Handle);
                return PartialView("DeletarProduto", produto);
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost(), ActionName("DeletarProduto")]
        public async Task<IActionResult> DeletarProdutoConfirmado(long Handle)
        {
            try
            {
                var produtoDeletado = await _produtoService.DeletarProdutoAsync(Handle);
                if (produtoDeletado)
                {
                    TempData["MensagemSucesso"] = "Produto deletado com sucesso!";
                }
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
