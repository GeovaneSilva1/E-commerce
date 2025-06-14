using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/condicaopagamento")]
    public class CondicaoPagamentoController : ControllerBase
    {
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;

        public CondicaoPagamentoController(ICondicaoPagamentoRepository condicaoPagamentoRepository)
        {
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
        }

        [HttpPost]
        [Route("AdminCadastrarCondicoesPag")]
        public IActionResult AddCondPagamento(string descricao, int dias)
        {
            bool existeCondPag = _condicaoPagamentoRepository.ExistByDescAndDias(descricao, dias);

            if (existeCondPag)
            {
                return BadRequest("Condição de pagamento já cadastrada!");
            }

            var condicaoPagamento = new CondicaoPagamento(descricao, dias);
            _condicaoPagamentoRepository.Add(condicaoPagamento);

            return Ok(condicaoPagamento);
        }

        [HttpGet]
        [Route("ObterCondPagamento")]
        public IActionResult GetCondPagamentos()
        {
            var condicaoPagamento = _condicaoPagamentoRepository.GetMany();

            if (condicaoPagamento is null)
            {
                return BadRequest("Nenhuma condição de pagamento cadastrada!");
            }

            var condPagamentoResponse = new CondicaoPagamentoResponse();
            condPagamentoResponse.IncluirAtributos(condicaoPagamento);
            return Ok(condPagamentoResponse);
        }
    }
}
