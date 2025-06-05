using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IVendaRepository
    {
        void Add(Venda venda);
    }
}
