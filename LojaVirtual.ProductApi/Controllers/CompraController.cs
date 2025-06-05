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
        private readonly IVendaRepository _vendaRepository;
        private readonly IVendaItemRepository _vendaItemRepository;

        public CompraController(IClienteRepository clienteRepository,
                               IProdutoRepository produtoRepository,
                               ICondicaoPagamentoRepository condicaoPagamentoRepository,
                               IVendaRepository vendaRepository,
                               IVendaItemRepository vendaItemRepository)
        {
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
            _vendaRepository = vendaRepository;   
            _vendaItemRepository = vendaItemRepository;
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

                if (itemcompra.Quantidade <= 0)
                {
                    return BadRequest($"Quantidade do produto {itemcompra.NomeProduto} inválida!");
                }

                if (!_produtoRepository.ExistBySKU(SKU))
                {
                    return BadRequest($"Produto {itemcompra.NomeProduto} não encontrado!");
                }

                produtos.Add(_produtoRepository.GetBySKU(SKU),itemcompra.Quantidade);
            }

            //Começa a persistir nas tabelas
            //tabela venda:
            var venda = new Venda(cliente.Id,condicaoPagamento.Id);
            _vendaRepository.Add(venda);
            
            //tabela vendaitens
            foreach (var prod in produtos)
            {
                decimal valorUnProduto = _produtoRepository.GetPrecoUnitarioById(prod.Key.Id);
                var vendaitem = new VendaItem(venda.Id,prod.Key.Id,prod.Value,(prod.Value * valorUnProduto));
                _vendaItemRepository.Add(vendaitem);
            }

            return Ok(compra);
        }

    }
}