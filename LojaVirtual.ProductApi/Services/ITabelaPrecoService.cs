using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public interface ITabelaPrecoService
    {
        Task<TabelaPrecoDTO> ValidaByDescricao(string descricaoTabelaPreco);
        Task AddTabelaPreco(TabelaPrecoDTO tabelaPrecoDTO);
        Task<IEnumerable<TabelaPrecoResponseDTO>> RetornaTabelaPrecosInseridas(IEnumerable<TabelaPrecoDTO> tabelaPrecoDTOs);
        Task<IEnumerable<TabelaPrecoResponseDTO>> GetTabelaPrecos();
        public TabelaPreco RetornaInstanciaByDTO(TabelaPrecoDTO tabelaPrecoDTO);
    }
}
