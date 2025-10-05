using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<IEnumerable<MarcaViewModel>> ObterMarcasAsync();
        Task<MarcaViewModel> ObterMarcaPorIdAsync(long handle);
        Task<MarcaViewModel> AdicionarMarcaAsync(MarcaViewModel marca);
        Task<MarcaViewModel> AtualizarMarcaAsync(MarcaViewModel marca);
        Task<bool> DeletarMarcaAsync(long handle);
    }
}
