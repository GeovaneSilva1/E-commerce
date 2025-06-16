using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface ITabelaPrecoRepository
    {
        Task<TabelaPrecoDTO> ValidaByDescricaoAsync(string descricaoTabelaPreco);
        Task Add(TabelaPreco tabelaPreco);
        Task<IEnumerable<TabelaPreco>> GetManyAsync();
    }
}
