using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class PrecoProdutoClienteRepository : IPrecoProdutoClienteRepository
    {
        private readonly AppDbContext _appDbContext;

        public PrecoProdutoClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(PrecoProdutoCliente precoProdutoCliente)
        {
            _appDbContext.PrecoProdutoClientes.Add(precoProdutoCliente);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
