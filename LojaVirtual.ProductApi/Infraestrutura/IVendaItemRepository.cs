using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IVendaItemRepository
    {
        Task Add(VendaItem vendaItem);
        Task<decimal> GetValorTotalVenda(int idVenda);
    }
}
