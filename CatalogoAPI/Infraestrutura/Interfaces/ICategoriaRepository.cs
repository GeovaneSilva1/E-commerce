using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<CategoriaDTO> ValidaByNome(string nomeCategoria);
        Task Add(Categoria categoria);
        Task<IEnumerable<Categoria>> GetMany();
        Task<Categoria> Get(long handle);
        Task Update(Categoria categoria);
        Task<bool> ExistsProdutosByCategoria(long handle);
        Task<Categoria> Delete(long handle);
    }
}
