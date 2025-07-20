using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.CatalogoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;

        public ProdutoController(IProdutoService produtoService, ICategoriaService categoriaService, IMarcaService marcaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
            _marcaService = marcaService;
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> AddProduto([FromBody] ProdutoDTO produtoDTO)
        {
            var produtoExistente = await _produtoService.GetProduto(produtoDTO.Handle);
            if (produtoExistente != null)
            {
                return BadRequest("Produto já existe.");
            }
            if (produtoDTO.Preco <= 0)
            {
                return BadRequest("Preço do produto deve ser maior que zero!");
            }

            var categoriaDTO = await _categoriaService.GetCategoria(produtoDTO.CategoriaId);
            var marcaDTO = await _marcaService.GetMarca(produtoDTO.MarcaId);

            await _produtoService.AddProduto(produtoDTO, categoriaDTO, marcaDTO);
            
            return Ok(produtoDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            var produtosDTO = await _produtoService.GetProdutos();
            
            if (!produtosDTO.Any())
            {
                return NotFound("Nenhum produto encontrado.");
            }   

            return Ok(produtosDTO);
        }
    }
}
