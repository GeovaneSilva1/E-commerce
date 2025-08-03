using LojaVirtual.CatalogoAPI.DTOs;

namespace LojaVirtual.CatalogoAPI.Services.Interfaces
{
    public interface IImagemProdutoService
    {   
        Task AddImagemProduto(FileUploadDTO fileUploadDTO, ProdutoDTO produtoDTO);
        Task UpdateImagemProduto(ImagemProdutoDTO imagemProdutoDTO, ProdutoDTO produtoDTO);
        Task<ImagemProdutoDTO> DeleteImagemProduto(long handle);
        Task<ImagemProdutoDTO> GetImagemProduto(long handle);
        Task<IEnumerable<ImagemProdutoDTO>> GetImagensProdutos();
        Task<IEnumerable<ImagemProdutoDTO>> GetImagensProdutoByProdutoHandle(long produtoHandle);
        Task<IEnumerable<ImagemProdutoDTO>> DeleteImagensProdutoByProdutoHandle(long produtoHandle);
        
    }
}
