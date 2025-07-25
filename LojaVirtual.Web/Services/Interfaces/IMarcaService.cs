using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IMarcaService
    {
          Task<IEnumerable<MarcaViewModel>> ObterMarcasAsync();
        //Task<MarcaViewModel> ObterMarcaPorIdAsync(int id);
        //Task<bool> AdicionarMarcaAsync(MarcaViewModel marca);
        //Task<bool> AtualizarMarcaAsync(MarcaViewModel marca);
        //Task<bool> ExcluirMarcaAsync(int id);
    }
}
