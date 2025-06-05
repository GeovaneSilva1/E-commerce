using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IClienteRepository
    {
        void Add(Cliente cliente);

        List<Cliente> Get();

        Cliente GetById(int id);
        Cliente GetByCNPJ(string CNPJ);

        bool ExistById(int id);
        bool ExistByCNPJ(string CNPJ);

        Cliente Update(Cliente cliente);

        Cliente DeleteById(int id);
    }
}
