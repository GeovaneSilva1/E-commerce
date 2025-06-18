using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public interface IVendaService
    {
        Task<VendaDTO> AddVenda(ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO);
    }
}
