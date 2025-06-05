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

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpPost]
        [Route("AdminCadastrarProduto")]
        public IActionResult AddProduto(int id, string nome, decimal preco)
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
                return BadRequest("Nome do produto inválido!");
            }

            var produto = new Produto(id, SKU, nome, preco);
            _produtoRepository.Add(produto);

            return Ok(produto);
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
