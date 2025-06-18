using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
    {
        private readonly AppDbContext _appDbContext;

        public CondicaoPagamentoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Add(CondicaoPagamento condicaoPagamento)
        {
            _appDbContext.CondicaoPagamentos.Add(condicaoPagamento);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CondicaoPagamento>> GetMany()
        {
            return await _appDbContext.CondicaoPagamentos.ToListAsync();
        }

        public async Task<CondicaoPagamento> GetByDescAndDias(string? descricao, int? dias)
        {
            return await _appDbContext.CondicaoPagamentos.Where(cp => cp.Descricao == descricao && cp.Dias == dias).FirstOrDefaultAsync();
        }
    }
}
