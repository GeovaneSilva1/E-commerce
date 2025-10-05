using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces
{
    public interface IProdutoRepository
    {
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task<Produto> Delete(long handle);
        Task<IEnumerable<Produto>> GetMany();
        Task<Produto> Get(long handle);
        Task<Produto> GetBySKU(string SKU);
        Task<bool> Exists(long handle);
        Task<IEnumerable<Produto>> GetByCategoriaId(long categoriaHandle);
    }
}
