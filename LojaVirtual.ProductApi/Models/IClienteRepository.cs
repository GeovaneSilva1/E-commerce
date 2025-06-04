namespace LojaVirtual.ProductApi.Models
{
    public interface IClienteRepository
    {
         void Add(Cliente cliente);
         
        List<Cliente> Get();

        Cliente GetById(int id);

        bool ExistById(int id);

        Cliente Update(Cliente cliente);

        Cliente DeleteById(int id);
    }
}
