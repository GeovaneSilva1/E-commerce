using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class VendaRepository : IVendaRepository
    {
        private readonly AppDbContext _appDbContext;

        public VendaRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(Venda venda)
        {
            _appDbContext.Vendas.Add(venda);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Venda> GetById(int id)
        {
            return await _appDbContext.Vendas.Include(v => v.Cliente)
                .Include(v => v.CondicaoPagamento)
                .Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public  List<VendaRelatorio> GetQueryRelatorioVendas(string CNPJ, string razaoSocial)
        {
            return _appDbContext.VendaRelatorios.FromSqlInterpolated($@"
            SELECT v.Id as IdVenda,
                   c.RazaoSocial as nomeCliente,
                   p.Descricao as produto,
                   vi.Quantidade,
                   vi.ValorUnitario as valor
            FROM vendas v
            inner join vendaitens vi on (v.Id = vi.VendaId)
            inner join produtos p on (vi.ProdutoId = p.Id )
            inner join clientes c on (v.ClienteId = c.Id )
            where c.CNPJ = {CNPJ} or c.RazaoSocial LIKE {'%' + razaoSocial + '%'}
            ").ToList();
        }
    }
}
