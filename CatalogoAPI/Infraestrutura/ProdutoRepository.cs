using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class ProdutoRepository : IProdutoRepository
    {
        public Task Add(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task Update(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Exists(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> GetBySKU(string SKU)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Produto>> GetMany()
        {
            throw new NotImplementedException();
        }

        
    }
}
