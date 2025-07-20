using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces
{
    public interface IImagemProdutoRepository
    {
        Task Add(ImagemProduto imagemProduto);
        Task Update(ImagemProduto imagemProduto);
        Task<ImagemProduto> Delete(long handle);
        Task<IEnumerable<ImagemProduto>> DeleteByProdutoHandle(long produtoHandle);
        Task<IEnumerable<ImagemProduto>> GetMany();
        Task<ImagemProduto> Get(long handle);
        Task<IEnumerable<ImagemProduto>> GetByProdutoHandle(long produtoHandle);
    }
}
