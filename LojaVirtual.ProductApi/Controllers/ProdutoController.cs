using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/produto")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ITabelaPrecoRepository _tabelaPrecoRepository;

        public ProdutoController(IProdutoRepository produtoRepository, ITabelaPrecoRepository tabelaPrecoRepository)
        {
            _produtoRepository = produtoRepository;
            _tabelaPrecoRepository = tabelaPrecoRepository;
        }

        [HttpPost]
        [Route("AdminCadastrarProduto")]
        public IActionResult AddProduto(int id, string nome, decimal preco, string descricaoTabelaPreco)
        {
            bool produtoExistente = _produtoRepository.ExistById(id);
            
            if (produtoExistente)
            {
                return BadRequest("Produto já cadastrado!");
            }

            if (preco <= 0)
            {
                return BadRequest("Preço do produto deve ser maior que zero!");
            }

            string SKU = MontaSKUByNome(nome);
            if (SKU.Equals(""))
            {
                return BadRequest("Nome do produto inválido! Ex: nome cor tamanho");
            }

            TabelaPreco tabelaPrecoValida = _tabelaPrecoRepository.ValidaByDescricao(descricaoTabelaPreco);
            
            if (tabelaPrecoValida is null)
            {
                return BadRequest("Tabela de preço Inexistente ou fora do período de aplicação!");
            }

            var produto = new Produto(id, SKU, nome, preco, tabelaPrecoValida.Id);
            _produtoRepository.Add(produto);

            return Ok(produto);
        }

        [HttpGet]
        [Route("ObterProdutos")]
        public IActionResult GetProdutos()
        {
            var produtos = _produtoRepository.GetMany();
            if (produtos is null)
            {
                return BadRequest("Nenhum produto cadastrado!");
            }

            var produtoResponse = new ProdutoResponse();
            produtoResponse.IncluirAtributos(produtos);

            return Ok(produtoResponse);
        }

        [HttpPut("{id:int}")]
        public IActionResult AlterarProduto(int id, [FromBody] ProdutoRequest produtoRequest)
        {
            Produto produto = _produtoRepository.GetById(id);
            
            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            var produtoAlterado = _produtoRepository.Update(produto, produtoRequest.NovoPreco);
            ProdutoResponse produtoResponse = new ProdutoResponse();
            produtoResponse.IncluirAtributos(produtoAlterado);
            
            return Ok(produtoResponse);
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
