using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface IClienteService
    {
        Task AddCliente(ClienteDTO clienteDTO);
        Task<IEnumerable<ClienteDTO>> GetClientes();
        Task<ClienteDTO> GetClienteById(int id);
        Task<bool> ExistCLienteById(int id);
        Task UpdateClienteById(ClienteDTO clienteDTO);
        Task<ClienteDTO> RemoveCliente(int id);
    }
}
