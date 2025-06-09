using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IPrecoProdutoClienteRepository
    {
        void Add(PrecoProdutoCliente precoProdutoCliente);
    }
}
