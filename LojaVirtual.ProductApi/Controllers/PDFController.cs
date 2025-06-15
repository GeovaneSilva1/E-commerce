using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/pdf")]
    public class PDFController : ControllerBase
    {
        private readonly IVendaRepository _VendaRepository;
        public PDFController(IVendaRepository vendaRepository)
        {
            _VendaRepository = vendaRepository;
        }

        [HttpGet]
        [Route("GerarRelatorioVendas")]
        public IActionResult GetPdf(string? cnpj, string? razaoSocial)
        {
            var pdfService = new GeracaoPDF(cnpj, razaoSocial, _VendaRepository);
            byte[] pdfBytes = pdfService.GerarPDF();
            var nomeArquivo = "RelatorioDeVendas.pdf";
            return File(pdfBytes,"application/pdf", nomeArquivo);
        }
    }
}
