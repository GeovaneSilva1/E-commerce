using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IMapper _mapper;
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IMapper mapper, IClienteRepository clienteRepository) 
        {
            _mapper = mapper;
            _clienteRepository = clienteRepository;
        }


        public async Task AddCliente(ClienteDTO clienteDTO)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteRepository.Add(cliente);
            clienteDTO.Id = cliente.Id;
        }

        public async Task<bool> ExistCLienteById(int id)
        {
            return await _clienteRepository.ExistById(id);
        }

        public async Task<ClienteDTO> GetClienteById(int id)
        {
            var cliente = await _clienteRepository.GetById(id);
            return _mapper.Map<ClienteDTO>(cliente);
        }

        public async Task<IEnumerable<ClienteDTO>> GetClientes()
        {
            var cliente = await _clienteRepository.Get();
            return _mapper.Map<IEnumerable<ClienteDTO>>(cliente);
        }

        public async Task<ClienteDTO> RemoveCliente(int id)
        {
            var clientedeletado  = await _clienteRepository.Delete(id);
            return _mapper.Map<ClienteDTO>(clientedeletado);
        }

        public async Task UpdateClienteById(ClienteDTO clienteDTO)
        {
            Cliente cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteRepository.Update(cliente);
        }
    }
}
