using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces
{
    public interface IMarcaRepository
    {
        Task Add(Marca marca);
        Task<IEnumerable<Marca>> GetMany();
        Task<Marca> Get(long handle);
        Task Update(Marca marca);
        Task<bool> ExistsProdutosByMarca(long handle);
        Task<Marca> Delete(long handle);
    }
}
