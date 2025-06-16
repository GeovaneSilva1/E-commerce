
using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface IProdutoService
    {
        Task AddProduto(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO);
        Task<ProdutoDTO> GetById(int id);
    }
}
