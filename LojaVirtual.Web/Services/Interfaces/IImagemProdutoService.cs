using LojaVirtual.Web.DTOs;
using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IImagemProdutoService
    {   Task<IEnumerable<ImagemProdutoViewModel>> ObterImagensPorProdutoIdAsync(long produtoHandle);
        //Task<ImagemProdutoViewModel> CriarImagemAsync(ImagemProdutoViewModel imagemProdutoViewModel);
        //Task<ImagemProdutoViewModel> AtualizarImagemAsync(ImagemProdutoViewModel imagemProdutoViewModel);
        //Task<bool> ExcluirImagemAsync(long imagemId);
        Task<IEnumerable<ImagemProdutoViewModel>> UploadImagensAsync(IEnumerable<IFormFile> fileUpload, long produtoId);
    }
}
