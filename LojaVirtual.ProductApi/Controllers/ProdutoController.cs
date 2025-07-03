using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using LojaVirtual.ProductApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly ITabelaPrecoService _tabelaPrecoService;

        public ProdutoController(IProdutoService produtoService, ITabelaPrecoService tabelaPrecoService)
        {
            _produtoService = produtoService;
            _tabelaPrecoService = tabelaPrecoService;
        }

        [HttpPost]
        [Route("AdminCadastrarProduto")]
        public async Task<ActionResult<ProdutoDTO>> AddProduto([FromBody] ProdutoDTO produtoDTO)
        {
            //Falta testar
            var produtoEncontrado = await _produtoService.GetById(produtoDTO.Id);
            
            if (produtoEncontrado is not null) 
            {
                return BadRequest("Produto já cadastrado!");
            }
            if (produtoDTO.Preco <= 0)
            {
                return BadRequest("Preço do produto deve ser maior que zero!");
            }

            string SKU = _produtoService.MontaSKUByNome(produtoDTO.Descricao);
            if (SKU.Equals(""))
            {
                return BadRequest("Nome do produto inválido! Ex: nome cor tamanho");
            }
            produtoDTO.SKU = SKU;

            TabelaPrecoDTO tabelaPrecoDTO = await _tabelaPrecoService.ValidaByDescricao(produtoDTO.DescricaoTabelaPreco);
            if (tabelaPrecoDTO is null)
            {
                return BadRequest("Tabela de preço Inexistente ou fora do período de aplicação!");
            }
            
            await _produtoService.AddProduto(produtoDTO, tabelaPrecoDTO);

            return Ok(produtoDTO);
        }

        [HttpGet]
        [Route("ObterProdutos")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
        {
            IEnumerable<ProdutoDTO> produtoDTO = await _produtoService.GetProdutos();

            if (produtoDTO.Count() <= 0)
            {
                return NotFound("Nenhum produto encontrado!");
            }

            return Ok(produtoDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> GetProduto(int id)
        {
            var ProdutoDTO = await _produtoService.GetById(id);
            if (ProdutoDTO is null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(ProdutoDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> EditProduto(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            bool prodEncontrado = await _produtoService.ExistProdutoById(id);
            if (!prodEncontrado)
            {
                return BadRequest("Produto não encontrado!");
            }

            if (produtoDTO.Preco <= 0)
            {
                return BadRequest("Preço do produto deve ser maior que zero!");
            }
            
            string SKU = _produtoService.MontaSKUByNome(produtoDTO.Descricao);
            if (SKU.Equals(""))
            {
                return BadRequest("Nome do produto inválido! Ex: nome cor tamanho");
            }

            TabelaPrecoDTO tabelaPrecoDTO = await _tabelaPrecoService.ValidaByDescricao(produtoDTO.DescricaoTabelaPreco);
            if (tabelaPrecoDTO is null)
            {
                return BadRequest("Tabela de preço Inexistente ou fora do período de aplicação!");
            }
            produtoDTO.SKU = SKU;
            produtoDTO.Id = id;
            await _produtoService.UpdateProdutoById(produtoDTO, tabelaPrecoDTO);
            
            return Ok(produtoDTO);
        }
    }
}
