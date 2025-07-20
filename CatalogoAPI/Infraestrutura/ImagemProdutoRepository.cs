using LojaVirtual.CatalogoAPI.Models;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class ImagemProdutoRepository : IImagemProdutoRepository
    {
        public Task Add(ImagemProduto imagemProduto)
        {
            throw new NotImplementedException();
        }

        public Task Update(ImagemProduto imagemProduto)
        {
            throw new NotImplementedException();
        }

        public Task<ImagemProduto> Delete(long handle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImagemProduto>> DeleteByProdutoHandle(long produtoHandle)
        {
            throw new NotImplementedException();
        }

        public Task<ImagemProduto> Get(long handle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImagemProduto>> GetByProdutoHandle(long produtoHandle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImagemProduto>> GetMany()
        {
            throw new NotImplementedException();
        }
    }
}
