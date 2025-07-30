using LojaVirtual.CatalogoAPI.DTOs;

namespace LojaVirtual.CatalogoAPI.Services.Interfaces
{
    public interface IMarcaService
    {
        Task AddMarca(MarcaDTO marcaDTO);
        Task<MarcaDTO> DeleteMarca(long handle);
        Task<bool> ExistProdutosByMarcas(long handle);
        Task<MarcaDTO> GetMarca(long handle);
        Task<IEnumerable<MarcaDTO>> GetMarcas();
        Task UpdateMarca(MarcaDTO marcaDTO);
    }
}
