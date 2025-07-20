using LojaVirtual.CatalogoAPI.DTOs;

namespace LojaVirtual.CatalogoAPI.Services.Interfaces
{
    public interface IMarcaService
    {
        Task AddMarca(MarcaDTO marcaDTO);
        Task<MarcaDTO> GetMarca(long handle);
        Task<IEnumerable<MarcaDTO>> GetMarcas();

    }
}
