using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using LojaVirtual.ProductApi.Controllers;
using System.Collections.Generic;
using LojaVirtual.ProductApi.Services;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/compra")]
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpPost]
        public async Task<ActionResult<VendaItemResponseDTO>> RealizarCompra([FromBody] CompraDTO compra)
        {
            ClienteDTO clienteDTO = await _compraService.RetornaClienteDTO(compra);
            CondicaoPagamentoDTO condicaoPagamentoDTO = await _compraService.RetornaCondicaoPagamentoDTO(compra);
            VendaDTO vendaDTO = await _compraService.CriaVendaItensVenda(compra.ItensDTO, clienteDTO, condicaoPagamentoDTO);
            if (vendaDTO.ErroVenda is not null)
            {
                return BadRequest(vendaDTO.ErroVenda);
            }

            VendaItemResponseDTO vendaItemResponseDTO = await _compraService.GetVendaItensResponseDTO(vendaDTO, compra.ItensDTO);
            return Ok(vendaItemResponseDTO);            
        }
    }
}