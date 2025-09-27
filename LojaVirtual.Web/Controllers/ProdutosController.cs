using LojaVirtual.Web.Models;
using LojaVirtual.Web.Models.Compostas;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LojaVirtual.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;
        private readonly IImagemProdutoService _ImagemProdutoService;

        public ProdutosController(IProdutoService produtoService,
                                  ICategoriaService categoriaService,
                                  IMarcaService marcaService,
                                  IImagemProdutoService imagemProdutoService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
            _ImagemProdutoService = imagemProdutoService;
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

        [HttpGet]
        public async Task<IActionResult> AdicionarImagemProduto(long Handle, [FromForm] ProdutoImagensViewModel produtoImagensViewModel)
        {
            var imagensProduto = await _ImagemProdutoService.ObterImagensPorProdutoIdAsync(Handle);
            var produto = await _produtoService.ObterProdutoPorIdAsync(Handle);
            produtoImagensViewModel.HandleProduto = produto.Handle;
            produtoImagensViewModel.NomeProduto = produto.Descricao;
            produtoImagensViewModel.ImagemProdutos = imagensProduto;

            return PartialView("AdicionarImagemProduto", produtoImagensViewModel);
        }

        [HttpPost(), ActionName("AdicionarImagemProduto")]
        public async Task<IActionResult> AdicionarImagemProduto([FromForm] ProdutoImagensViewModel produtoImagensViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Nenhuma imagem selecionada." });
            }

            try
            {
                var imagensAdicionadas = await _ImagemProdutoService.UploadImagensAsync(produtoImagensViewModel.Files, produtoImagensViewModel.HandleProduto);
                string htmlAtualizado = await RenderPartialViewToString("_ListaImagens", imagensAdicionadas);

                return Json(new { success = true, html = htmlAtualizado, message = "Imagens enviadas com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost(), ActionName("DeleteImages")]
        public async Task<IActionResult> DeleteImages(long imageHandle)
        {
            try
            {
                IEnumerable<ImagemProdutoViewModel> imagensRecarregadas = await _ImagemProdutoService.DeletarImagemAsync(imageHandle);
                string htmlAtualizado = await RenderPartialViewToString("_ListaImagens", imagensRecarregadas);

                return Json(new { success = true, html = htmlAtualizado, message = "Imagem deletada com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private async Task<string> RenderPartialViewToString(string htmlName, IEnumerable<ImagemProdutoViewModel> imagensAdicionadas)
        {
            var viewEngine = HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
            var viewResult = viewEngine.FindView(ControllerContext, htmlName, false);

            if (!viewResult.Success)
                throw new FileNotFoundException($"View '{htmlName}' não encontrada.");

            ViewData.Model = imagensAdicionadas;

            using (var sw = new StringWriter())
            {
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
