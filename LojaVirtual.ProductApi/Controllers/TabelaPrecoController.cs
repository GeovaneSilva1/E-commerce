using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/tabelapreco")]
    public class TabelaPrecoController : ControllerBase
    {
        private readonly ITabelaPrecoRepository _tabelaPrecoRepository;

        public TabelaPrecoController(ITabelaPrecoRepository tabelaPrecoRepository)
        {
            _tabelaPrecoRepository = tabelaPrecoRepository;
        }

        [HttpPost]
        [Route("AdminCadastrarTabelaPreco")]
        public IActionResult AddTabelaPreco([FromBody] List<TabelaPrecoRequest> tabelaPrecos)
        {
            List<TabelaPreco> tabelaPrecosInseridas = RetornaTabelaPrecosInseridas(tabelaPrecos);

            return Ok(tabelaPrecosInseridas);
        }

        [HttpGet]
        [Route("AdminObterTabelaPrecos")]
        public IActionResult ObterTabelaPrecos()
        {
            var tabelaPrecos = _tabelaPrecoRepository.GetMany();
            if (tabelaPrecos is null)
            {
                return BadRequest("Nenhuma tabela de preço encontrada!");
            }

            return Ok(tabelaPrecos);
        }

        private List<TabelaPreco> RetornaTabelaPrecosInseridas(List<TabelaPrecoRequest> tabelaPrecosRequests)
        {
            List<TabelaPreco> tabPrecos = new List<TabelaPreco>();
            foreach (var tabelaprecoRequest in tabelaPrecosRequests)
            {
                //TabelaPreco novaTabelaPreco = new TabelaPreco(tabelaprecoRequest.Descricao, DateTime.Now, RetornaDataByDias(tabelaprecoRequest.DiasValidos));
                //_tabelaPrecoRepository.Add(novaTabelaPreco);

                //tabPrecos.Add(novaTabelaPreco);
            }

            return tabPrecos;
        }

        private DateTime RetornaDataByDias(int dias)
        {
            DateTime DataFim = DateTime.Now.AddDays(dias);
            return DataFim;
        }
    }
}
