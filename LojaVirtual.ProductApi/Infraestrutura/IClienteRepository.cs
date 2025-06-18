using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IClienteRepository
    {
        Task<Cliente> Add(Cliente cliente);
        Task<Cliente> GetById(int id);
        Task<IEnumerable<Cliente>> Get();
        Task<Cliente> GetByCNPJ(string CNPJ);
        Task<bool> ExistById(int id);
        Task<Cliente> Update(Cliente cliente);
        Task<Cliente> Delete(int id);
    }
}
