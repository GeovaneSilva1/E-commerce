using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class TabelaPrecoRepository : ITabelaPrecoRepository
    {
        private readonly AppDbContext _appDbContext;

        public TabelaPrecoRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(TabelaPreco tabelaPreco)
        {
            _appDbContext.TabelaPrecos.Add(tabelaPreco);
            _appDbContext.SaveChanges();
        }

        public List<TabelaPreco> GetMany()
        {
            return _appDbContext.TabelaPrecos.ToList();
        }

        public TabelaPreco ValidaByDescricao(string descricaoTabelaPreco)
        {
            return _appDbContext.TabelaPrecos.Where(tp => tp.DataFim >= DateTime.Now && tp.Descricao.Contains(descricaoTabelaPreco)).FirstOrDefault();
        }
    }
}
