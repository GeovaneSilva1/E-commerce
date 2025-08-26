using LojaVirtual.Web.Models;
using LojaVirtual.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Web.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;
        private readonly IImagemProdutoService _imagemProdutoService;

        public CatalogoController(IProdutoService produtoService,
                                 ICategoriaService categoriaService,
                                 IMarcaService marcaService,
                                 IImagemProdutoService imagemProdutoService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
            _imagemProdutoService = imagemProdutoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pagina = 1, int itensPorPagina = 9)
        {
            var produtos = await _produtoService.ObterProdutosAsync();
            
            // Aplicar paginação
            var produtosPaginados = produtos
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();

            // Obter categorias para filtro
            ViewBag.Categorias = await _categoriaService.ObterCategoriasAsync();
            ViewBag.Marcas = await _marcaService.ObterMarcasAsync();
            
            // Configurar paginação
            ViewBag.PaginaAtual = pagina;
            ViewBag.ItensPorPagina = itensPorPagina;
            ViewBag.TotalItens = produtos.Count();
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)produtos.Count() / itensPorPagina);

            return View(produtosPaginados);
        }

        [HttpGet]
        public async Task<IActionResult> Filtrar(long? categoriaId, long? marcaId, int pagina = 1, int itensPorPagina = 9)
        {
            var produtos = await _produtoService.ObterProdutosAsync();

            // Aplicar filtros
            if (categoriaId.HasValue)
            {
                produtos = produtos.Where(p => p.CategoriaId == categoriaId.Value).ToList();
            }

            if (marcaId.HasValue)
            {
                produtos = produtos.Where(p => p.MarcaId == marcaId.Value).ToList();
            }

            // Aplicar paginação
            var produtosPaginados = produtos
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();

            // Obter categorias para filtro
            ViewBag.Categorias = await _categoriaService.ObterCategoriasAsync();
            ViewBag.Marcas = await _marcaService.ObterMarcasAsync();
            ViewBag.CategoriaIdSelecionada = categoriaId;
            ViewBag.MarcaIdSelecionada = marcaId;
            
            // Configurar paginação
            ViewBag.PaginaAtual = pagina;
            ViewBag.ItensPorPagina = itensPorPagina;
            ViewBag.TotalItens = produtos.Count();
            ViewBag.TotalPaginas = (int)Math.Ceiling((double)produtos.Count() / itensPorPagina);

            return View("Index", produtosPaginados);
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(long handle)
        {
            var produto = await _produtoService.ObterProdutoPorIdAsync(handle);
            if (produto == null)
            {
                return NotFound();
            }

            // Obter imagens do produto
            var imagens = await _imagemProdutoService.ObterImagensPorProdutoIdAsync(handle);
            ViewBag.Imagens = imagens;

            return View(produto);
        }
    }
}