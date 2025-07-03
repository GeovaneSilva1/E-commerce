using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class PrecoProdutoClienteService : IPrecoProdutoClienteService
    {
        private readonly IPrecoProdutoClienteRepository _precoProdutoClienteRepository;
        private readonly IMapper _mapper;

        public PrecoProdutoClienteService(IPrecoProdutoClienteRepository precoProdutoClienteRepository, IMapper mapper)
        {
            _precoProdutoClienteRepository = precoProdutoClienteRepository;
            _mapper = mapper;
        }

        public async Task AddPrecoProdutoClientes(PrecoProdutoClienteDTO precoProdutoClienteDTO)
        {
            PrecoProdutoCliente precoProdutoCliente = _mapper.Map<PrecoProdutoCliente>(precoProdutoClienteDTO);
            await _precoProdutoClienteRepository.Add(precoProdutoCliente);

        }
    }
}
