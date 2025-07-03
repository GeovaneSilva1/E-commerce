using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using LojaVirtual.ProductApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/tabelapreco")]
    public class TabelaPrecoController : ControllerBase
    {
        private readonly ITabelaPrecoService _tabelaPrecoService;

        public TabelaPrecoController(ITabelaPrecoService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
        }

        [HttpPost]
        [Route("CadastrarTabelaPreco")]
        public async Task<ActionResult<IEnumerable<TabelaPrecoResponseDTO>>> AddTabelaPreco([FromBody] IEnumerable<TabelaPrecoDTO> tabelaPrecos)
        {
            //falta testar
            var tabelaPrecosInseridas = await _tabelaPrecoService.RetornaTabelaPrecosInseridas(tabelaPrecos);

            return Ok(tabelaPrecosInseridas);
        }

        [HttpGet]
        [Route("ObterTabelaPrecos")]
        public async Task<ActionResult<IEnumerable<TabelaPrecoResponseDTO>>> ObterTabelaPrecos()
        {
            //falta testar
            IEnumerable<TabelaPrecoResponseDTO> tabelaPrecoResponseDTO = await _tabelaPrecoService.GetTabelaPrecos();
            if (tabelaPrecoResponseDTO.Count() <= 0)
                return NotFound("Nenhuma tabela de preço encontrada!");

            return Ok(tabelaPrecoResponseDTO);
        }
    }
}
