using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface IPrecoProdutoClienteService
    {
        Task AddPrecoProdutoClientes(PrecoProdutoClienteDTO precoProdutoClienteDTO);
    }
}
