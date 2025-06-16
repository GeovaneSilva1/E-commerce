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

            string SKU = MontaSKUByNome(produtoDTO.Descricao);
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
        public IActionResult GetProdutos()
        {
          /*  var produtos = _produtoRepository.GetMany();
            if (produtos is null)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            var produtoResponse = new ProdutoResponse();
            produtoResponse.IncluirAtributos(produtos);
          */
            return Ok();
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
        public IActionResult AlterarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
         /*   Produto produto = _produtoRepository.GetById(id);
            
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoAlterado = _produtoRepository.Update(produto, produtoRequest.NovoPreco);
            ProdutoResponse produtoResponse = new ProdutoResponse();
            produtoResponse.IncluirAtributos(produtoAlterado);
            */
            return Ok(); 
        }

        public static string MontaSKUByNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return string.Empty;

            var palavras = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (palavras.Length < 3)
                return string.Empty;

            string primeira = palavras[0].Length >= 3 ? palavras[0].Substring(0, 3) : palavras[0];
            string segunda = palavras[1].Length >= 2 ? palavras[1].Substring(0, 2) : palavras[1];
            string terceira = palavras[2].Substring(0, 1);

            return $"{primeira.ToUpper()}-{segunda.ToUpper()}-{terceira.ToUpper()}";

        }
    }
}
