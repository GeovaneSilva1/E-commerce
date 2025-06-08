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
            Cliente cliente = RetornaCliente(compra);
            CondicaoPagamento condicaoPagamento = RetornaCondicaoPagamento(compra);

            Dictionary<Produto, int> produtos = new Dictionary<Produto, int>();
            foreach (var itemcompra in compra.Itens)
            {
                string SKU = ProdutoController.MontaSKUByNome(itemcompra.NomeProduto);
                if (SKU.Equals(""))
                {
                    return BadRequest($"Nome do produto {itemcompra.NomeProduto} inválido! Ex: nome cor tamanho");
                }

                if (itemcompra.Quantidade <= 0)
                {
                    return BadRequest($"Quantidade do produto {itemcompra.NomeProduto} inválida!");
                }

                if (!_produtoRepository.ExistBySKU(SKU))
                {
                    return BadRequest($"Produto {itemcompra.NomeProduto} não encontrado!");
                }

                produtos.Add(_produtoRepository.GetBySKU(SKU), itemcompra.Quantidade);
            }

            Venda venda = CriaVenda(cliente, condicaoPagamento);

            //tabela vendaitens
            CriaVendaItens(produtos, venda);
            decimal valorTotalVenda = _vendaItemRepository.GetValorTotalVenda(venda.Id);
            
            CompraResponse compraResponse = new CompraResponse(compra,valorTotalVenda);

            return Ok(compraResponse);
        }

        private void CriaVendaItens(Dictionary<Produto, int> produtos, Venda venda)
        {
            foreach (var prod in produtos)
            {
                decimal valorUnProduto = _produtoRepository.GetPrecoUnitarioById(prod.Key.Id);
                var vendaitem = new VendaItem(venda.Id, prod.Key.Id, prod.Value, (prod.Value * valorUnProduto));
                _vendaItemRepository.Add(vendaitem);
            }
        }

        private Venda CriaVenda(Cliente cliente, CondicaoPagamento condicaoPagamento)
        {
            var venda = new Venda(cliente.Id, condicaoPagamento.Id);
            _vendaRepository.Add(venda);
            return venda;
        }

        private CondicaoPagamento RetornaCondicaoPagamento(Compra compra)
        {
            CondicaoPagamento condicaoPagamento = _condicaoPagamentoRepository.GetByByDescAndDias(compra.CondicaoPagamento.Descricao, compra.CondicaoPagamento.Dias);
            if (condicaoPagamento is null)
            {
                condicaoPagamento = new CondicaoPagamento(compra.CondicaoPagamento.Descricao, compra.CondicaoPagamento.Dias);
                _condicaoPagamentoRepository.Add(condicaoPagamento);
            }

            return condicaoPagamento;
        }

        private Cliente RetornaCliente(Compra compra)
        {
            Cliente cliente = _clienteRepository.GetByCNPJ(compra.CpfCnpj);
            if (cliente is null)
            {
                cliente = new Cliente(compra.CpfCnpj, compra.NomeRazao, compra.Email);
                _clienteRepository.Add(cliente);
            }

            return cliente;
        }
    }
}