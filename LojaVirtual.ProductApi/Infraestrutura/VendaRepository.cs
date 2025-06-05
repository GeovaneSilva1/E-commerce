using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class VendaRepository : IVendaRepository
    {
        private readonly AppDbContext _appDbContext;

        public VendaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Venda venda)
        {
            _appDbContext.Vendas.Add(venda);
            _appDbContext.SaveChanges();
        }
    }
}
