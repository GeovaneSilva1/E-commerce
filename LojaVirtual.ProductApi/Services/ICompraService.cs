using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface ICompraService
    {
        Task<ClienteDTO> RetornaClienteDTO(CompraDTO compra);
        Task<CondicaoPagamentoDTO> RetornaCondicaoPagamentoDTO(CompraDTO compra);
        Task<VendaDTO> RetornaVendaDTO(ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO);
        Task<VendaItemResponseDTO> GetVendaItensResponseDTO(VendaDTO vendaDTO,IEnumerable<VendaItemDTO>? itensDTO);
        Task<VendaDTO> CriaVendaItensVenda(IEnumerable<VendaItemDTO>? itensDTO, ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO);
    }
}
