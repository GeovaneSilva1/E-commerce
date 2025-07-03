using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface IVendaItemService
    {
        Task AddVendaItens(IEnumerable<VendaItemDTO> itensDTO);
        Task<decimal> GetValorTotalVenda(int id);
    }
}
