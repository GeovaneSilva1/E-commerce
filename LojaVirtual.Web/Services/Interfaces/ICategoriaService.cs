using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync();
        Task<CategoriaViewModel> ObterCategoriaPorIdAsync(long handle);
        Task<CategoriaViewModel> AdicionarCategoriaAsync(CategoriaViewModel categoria);
        Task<CategoriaViewModel> AtualizarCategoriaAsync(CategoriaViewModel categoria);
        //Task<bool> ExcluirCategoriaAsync(int id);
    }
}
