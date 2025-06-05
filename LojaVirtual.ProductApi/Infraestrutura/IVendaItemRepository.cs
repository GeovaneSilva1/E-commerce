using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IVendaItemRepository
    {
        void Add(VendaItem vendaItem);
    }
}
