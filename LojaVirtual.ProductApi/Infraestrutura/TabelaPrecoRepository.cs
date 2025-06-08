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

        public TabelaPreco ValidaByDescricao(string descricaoTabelaPreco)
        {
            return _appDbContext.TabelaPrecos.Where(tp => tp.DataFim >= DateTime.Now && tp.Descricao.Contains(descricaoTabelaPreco)).FirstOrDefault();
        }
    }
}
