using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
    {
        private readonly AppDbContext _appDbContext;

        public CondicaoPagamentoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(CondicaoPagamento condicaoPagamento)
        {
            _appDbContext.CondicaoPagamentos.Add(condicaoPagamento);
            _appDbContext.SaveChanges();
        }

        public bool ExistById(int id)
        {
            return _appDbContext.CondicaoPagamentos.Any(cp => cp.Id == id);
        }

        public bool ExistByDescAndDias(string descricao, string dias)
        {
            return _appDbContext.CondicaoPagamentos.Any(cp => cp.Descricao == descricao && cp.Dias == dias);
        }

        public CondicaoPagamento GetById(int id)
        {
            return _appDbContext.CondicaoPagamentos.Where(cp => cp.Id == id).FirstOrDefault();
        }

        public CondicaoPagamento GetByByDescAndDias(string descricao, string dias)
        {
            return _appDbContext.CondicaoPagamentos.Where(cp => cp.Descricao == descricao && cp.Dias == dias).FirstOrDefault();
        }
    }
}
