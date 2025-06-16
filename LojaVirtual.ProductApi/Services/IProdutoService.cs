
using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface IProdutoService
    {
        Task AddProduto(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO);
        Task<ProdutoDTO> GetById(int id);
        Task<IEnumerable<ProdutoDTO>> GetProdutos();
        Task<bool> ExistProdutoById(int id);
        Task UpdateProdutoById(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO);
        string MontaSKUByNome(string? nome);
        
    }
}
