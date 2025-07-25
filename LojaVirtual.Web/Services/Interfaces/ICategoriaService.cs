using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> ObterCategoriasAsync();
        //Task<CategoriaViewModel> ObterCategoriaPorIdAsync(int id);
        //Task<bool> AdicionarCategoriaAsync(CategoriaViewModel categoria);
        //Task<bool> AtualizarCategoriaAsync(CategoriaViewModel categoria);
        //Task<bool> ExcluirCategoriaAsync(int id);
    }
}
