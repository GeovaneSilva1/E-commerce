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

        public bool ExistById(int id)
        {
            return _appDbContext.CondicaoPagamentos.Any(cp => cp.Id == id);
        }

        public bool ExistByDescAndDias(string descricao, int? dias)
        {
            return _appDbContext.CondicaoPagamentos.Any(cp => cp.Descricao == descricao && cp.Dias == dias);
        }

        public CondicaoPagamento GetById(int id)
        {
            return _appDbContext.CondicaoPagamentos.Where(cp => cp.Id == id).FirstOrDefault();
        }
    }
}
