using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IImagemProdutoService
    {
        Task<IEnumerable<ImagemProdutoViewModel>> DeletarImagemAsync(long imageHandle);
        Task<IEnumerable<ImagemProdutoViewModel>> ObterImagensPorProdutoIdAsync(long produtoHandle);
        Task<IEnumerable<ImagemProdutoViewModel>> UploadImagensAsync(IEnumerable<IFormFile> fileUpload, long produtoId);
    }
}
