using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using LojaVirtual.ProductApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/condicaopagamento")]
    public class CondicaoPagamentoController : ControllerBase
    {
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;
        private readonly ICondicaoPagamentoService _condicaoPagamentoService;

        public CondicaoPagamentoController(ICondicaoPagamentoRepository condicaoPagamentoRepository, 
                                           ICondicaoPagamentoService condicaoPagamentoService)
        {
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
            _condicaoPagamentoService = condicaoPagamentoService;
        }

        [HttpPost]
        [Route("CadastrarCondPagamento")]
        public async Task<ActionResult<CondicaoPagamentoDTO>> AddCondPagamento([FromBody] CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            CondicaoPagamentoDTO condPagDTO = await _condicaoPagamentoService.GetCondPagamentoByDescAndDias(condicaoPagamentoDTO.Descricao, condicaoPagamentoDTO.Dias);
            if (condPagDTO is not null)
            {
                return BadRequest("Condição de pagamento já cadastrada!");
            }

            await _condicaoPagamentoService.AddCondicaoPagamento(condicaoPagamentoDTO);

            return Ok(condicaoPagamentoDTO);
        }

        [HttpGet]
        [Route("ObterCondPagamento")]
        public async Task<ActionResult<IEnumerable<CondicaoPagamentoDTO>>> GetCondPagamentos()
        {
            IEnumerable<CondicaoPagamentoDTO> condicaoPagamentoDTO = await _condicaoPagamentoService.GetCondicoesPagamento();

            if (condicaoPagamentoDTO.Count() <= 0)
            {
                return NotFound("Nenhuma condição de pagamento encontrada!");
            }

            return Ok(condicaoPagamentoDTO);
        }
    }
}
