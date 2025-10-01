using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.CatalogoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private readonly IMarcaService _marcaService;

        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService, IMarcaService marcaService)
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
            if (produtoDTO.Estoque <= 0)
            {
                return BadRequest("Estoque do produto não pode ser negativo!");
            }

            var categoriaDTO = await _categoriaService.GetCategoria(produtoDTO.CategoriaId);
            var marcaDTO = await _marcaService.GetMarca(produtoDTO.MarcaId);

            await _produtoService.AddProduto(produtoDTO, categoriaDTO, marcaDTO);

            return Ok(produtoDTO);
        }

        [HttpPut]
        public async Task<ActionResult<ProdutoDTO>> UpdateProduto([FromBody] ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
            {
                return BadRequest("Dados do produto inválidos.");
            }
            if (produtoDTO.Preco <= 0)
            {
                return BadRequest("Preço do produto deve ser maior que zero!");
            }
            if (produtoDTO.Descricao is null)
            {
                return BadRequest("Descrição do produto não pode ser vazia.");
            }
            var categoriaDTO = await _categoriaService.GetCategoria(produtoDTO.CategoriaId);
            var marcaDTO = await _marcaService.GetMarca(produtoDTO.MarcaId);

            if (categoriaDTO is null)
            {
                return NotFound($"Categoria com id {produtoDTO.CategoriaId} não encontrada.");
            }

            if (marcaDTO is null)
            {
                return NotFound($"Marca com id {produtoDTO.MarcaId} não encontrada.");
            }

            await _produtoService.UpdateProduto(produtoDTO, categoriaDTO, marcaDTO);

            return Ok(produtoDTO);
        }

        [HttpDelete("{handle}")]
        public async Task<ActionResult<ProdutoDTO>> DeleteProduto(long handle)
        {
            var produtoDTO = await _produtoService.GetProduto(handle);

            if (produtoDTO is null)
            {
                return NotFound($"Produto com id {handle} não encontrado.");
            }
            if (produtoDTO.Estoque > 0)
            {
                return BadRequest("Não é possível excluir um produto que ainda possui estoque.");
            }

            produtoDTO = await _produtoService.DeleteProduto(handle);

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

        [HttpGet("{handle}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(long handle)
        {
            var produtoDTO = await _produtoService.GetProduto(handle);
            if (produtoDTO is null)
            {
                return NotFound($"Produto com id {handle} não encontrado.");
            }
            return Ok(produtoDTO);
        }

        [HttpGet("categoria/{categoriaHandle}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosByCategoria(long categoriaHandle)
        {
            var produtosDTO = await _produtoService.GetProdutosByCategoriaId(categoriaHandle);
            
            if (produtosDTO is null || !produtosDTO.Any())
            {
                return NotFound($"Nenhum produto encontrado para a categoria com id {categoriaHandle}.");
            }

            return Ok(produtosDTO);
        }
    }
}
