using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.ProductApi.Controllers;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/compra")]
    public class CompraController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;

        public CompraController(IClienteRepository clienteRepository,
                               IProdutoRepository produtoRepository,
                               ICondicaoPagamentoRepository condicaoPagamentoRepository)
        {
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
        }

        [HttpPost]
        public IActionResult RealizarCompra([FromBody] Compra compra)
        {
            Cliente cliente = _clienteRepository.GetByCNPJ(compra.CpfCnpj);
            if (cliente is null)
            {
                cliente = new Cliente(compra.CpfCnpj, compra.NomeRazao);
                _clienteRepository.Add(cliente);
            }

            CondicaoPagamento condicaoPagamento = _condicaoPagamentoRepository.GetByByDescAndDias(compra.CondicaoPagamento.Descricao, compra.CondicaoPagamento.Dias);
            if (condicaoPagamento is null)
            {
                condicaoPagamento = new CondicaoPagamento(compra.CondicaoPagamento.Descricao, compra.CondicaoPagamento.Dias);
                _condicaoPagamentoRepository.Add(condicaoPagamento);
            }

            Dictionary<Produto,int> produtos = new Dictionary<Produto,int>();
            foreach (var itemcompra in compra.Itens)
            {
                string SKU = ProdutoController.MontaSKUByNome(itemcompra.NomeProduto);
                if (SKU.Equals(""))
                {
                    return BadRequest($"Nome do produto {itemcompra.NomeProduto} inválido!");
                }
                produtos.Add(_produtoRepository.GetBySKU(SKU),itemcompra.Quantidade);
            }

            //Começa a persistir nas tabelas
            //tabela venda:
            

            return Ok(compra);
        }

    }
}