using LojaVirtual.ProductApi.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/venda")]
    public class VendaController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;

        public VendaController(IClienteRepository clienteRepository, 
                               IProdutoRepository produtoRepository, 
                               ICondicaoPagamentoRepository condicaoPagamentoRepository)
        {
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
        }

        [HttpPost]
        [Route("RealizarCompra")]
        public IActionResult AddVenda(string cnpj, string razaoSocial, string nomeProduto, int quantidadeProduto, decimal valor)
        {

            return Ok();
        }

    }
}
