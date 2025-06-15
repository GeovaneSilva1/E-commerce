using LojaVirtual.ProductApi.DTOs;
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

        public async Task<Cliente> Add(Cliente cliente)
        {
            _appDbContext.Clientes.Add(cliente);
            await _appDbContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> ExistById(int id)
        {
            return await _appDbContext.Clientes.AnyAsync(c => c.Id == id);
        }

        /*public Cliente Update(Cliente cliente, ClienteDTO clienteRequest)
        {
            _appDbContext.Entry(cliente).State = EntityState.Modified;
            cliente.CNPJ = clienteRequest.CNPJ;
            cliente.RazaoSocial = clienteRequest.RazaoSocial;
            cliente.Email = clienteRequest.Email;

            _appDbContext.SaveChanges();
            return cliente;
        } */

        /*public Cliente DeleteById(int id)
        {
            var cliente = GetById(id);
            _appDbContext.Clientes.Remove(cliente);
            _appDbContext.SaveChanges();
            return cliente;
        } */

        public async Task<Cliente> GetById(int id)
        { 
            return await _appDbContext.Clientes.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Cliente GetByCNPJ(string CNPJ)
        {
            return _appDbContext.Clientes.Where(c => c.CNPJ == CNPJ).FirstOrDefault();
        }

        public bool ExistByCNPJ(string CNPJ)
        {
            return _appDbContext.Clientes.Any(c => c.CNPJ == CNPJ);
        }

        public async Task<Cliente> Delete(int id)
        {
            var cliente = await GetById(id);
            _appDbContext.Clientes.Remove(cliente);
            await _appDbContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> Get()
        {
            return await _appDbContext.Clientes.ToListAsync();
        }

        public async Task<Cliente> Update(Cliente cliente)
        {
            _appDbContext.Entry(cliente).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return cliente;
        }
    }
}
