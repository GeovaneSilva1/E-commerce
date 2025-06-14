using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class VendaItemRepository : IVendaItemRepository
    {
        private readonly AppDbContext _appDbContext;

        public VendaItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(VendaItem vendaitem)
        {
            _appDbContext.VendaItens.Add(vendaitem);
            _appDbContext.SaveChanges();
        }

        public decimal GetValorTotalVenda(int idVenda)
        {
            return _appDbContext.VendaItens.Where(v => v.Venda.Id == idVenda).Sum(v => v.Valor);
        }
    }
}
