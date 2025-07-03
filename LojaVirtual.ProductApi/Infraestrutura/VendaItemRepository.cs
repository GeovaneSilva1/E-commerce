using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class VendaItemRepository : IVendaItemRepository
    {
        private readonly AppDbContext _appDbContext;

        public VendaItemRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(VendaItem vendaitem)
        {
            _appDbContext.VendaItens.Add(vendaitem);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<decimal> GetValorTotalVenda(int idVenda)
        {
            return await _appDbContext.VendaItens.Where(v => v.Venda.Id == idVenda).SumAsync(v => v.Valor);
        }
    }
}
