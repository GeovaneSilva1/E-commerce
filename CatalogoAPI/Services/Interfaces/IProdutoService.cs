using LojaVirtual.CatalogoAPI.DTOs;

namespace LojaVirtual.CatalogoAPI.Services.Interfaces
{
    public interface IProdutoService
    {
        Task AddProduto(ProdutoDTO produtoDTO, CategoriaDTO categoriaDTO, MarcaDTO marcaDTO);
        Task UpdateProduto(ProdutoDTO produtoDTO, CategoriaDTO categoriaDTO, MarcaDTO marcaDTO);
        Task<ProdutoDTO> DeleteProduto(long handle);
        Task<ProdutoDTO> GetProduto(long handle);
        Task<IEnumerable<ProdutoDTO>> GetProdutos();
        Task<ProdutoDTO> GetBySKU(string SKU);
        Task<bool> ExistProduto(long handle);
    }
}
