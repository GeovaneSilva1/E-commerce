using LojaVirtual.CatalogoAPI.DTOs;

namespace LojaVirtual.CatalogoAPI.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task AddCategoria(CategoriaDTO categoriaDTO);
        Task<CategoriaDTO> GetCategoria(long handle);
        Task<IEnumerable<CategoriaDTO>> GetCategorias();
    }
}
