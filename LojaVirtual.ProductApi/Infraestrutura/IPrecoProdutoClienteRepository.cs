using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IPrecoProdutoClienteRepository
    {
        Task Add(PrecoProdutoCliente precoProdutoCliente);
    }
}
