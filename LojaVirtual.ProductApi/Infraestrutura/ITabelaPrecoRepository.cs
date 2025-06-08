using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface ITabelaPrecoRepository
    {
        TabelaPreco ValidaByDescricao(string descricaoTabelaPreco);
    }
}
