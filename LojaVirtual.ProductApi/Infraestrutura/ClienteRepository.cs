using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _appDbContext;

        public ClienteRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Cliente cliente)
        {
            _appDbContext.Clientes.Add(cliente);
            _appDbContext.SaveChanges();
        }

        public List<Cliente> Get()
        {
            return _appDbContext.Clientes.ToList();
        }

        public bool ExistById(int id)
        {
            return _appDbContext.Clientes.Any(c => c.Id == id);
        }

        public Cliente Update(Cliente cliente)
        {
            _appDbContext.Entry(cliente).State = EntityState.Modified;
            _appDbContext.SaveChanges();
            return cliente;
        }

        public Cliente DeleteById(int id)
        {
            var cliente = GetById(id);
            _appDbContext.Clientes.Remove(cliente);
            _appDbContext.SaveChanges();
            return cliente;
        }

        public Cliente GetById(int id)
        {
            return _appDbContext.Clientes.Where(c => c.Id == id).FirstOrDefault();
        }

        public Cliente GetByCNPJ(string CNPJ)
        {
            return _appDbContext.Clientes.Where(c => c.CNPJ == CNPJ).FirstOrDefault();
        }

        public bool ExistByCNPJ(string CNPJ)
        {
            return _appDbContext.Clientes.Any(c => c.CNPJ == CNPJ);
        }
    }
}
