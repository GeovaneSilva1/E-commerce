using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IMarcaService
    {
          Task<IEnumerable<MarcaViewModel>> ObterMarcasAsync();
        //Task<MarcaViewModel> ObterMarcaPorIdAsync(int id);
        Task<MarcaViewModel> AdicionarMarcaAsync(MarcaViewModel marca);
        //Task<bool> AtualizarMarcaAsync(MarcaViewModel marca);
        //Task<bool> DeletarMarcaAsync(int id);
    }
}
