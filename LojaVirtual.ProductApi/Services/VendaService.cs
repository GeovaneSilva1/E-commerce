using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class VendaService : IVendaService
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly IMapper _mapper;

        public VendaService(IVendaRepository vendaRepository, IMapper mapper)
        {
            _vendaRepository = vendaRepository;
            _mapper = mapper;
        }

        public async Task<VendaDTO> AddVenda(ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            VendaDTO vendaDTO = new VendaDTO();
            vendaDTO.ClienteId = clienteDTO.Id;
            vendaDTO.CondicaoPagamentoId = condicaoPagamentoDTO.Id;
            var mapVenda = _mapper.Map<Venda>(vendaDTO);
            await _vendaRepository.Add(mapVenda);

            Venda venda = await _vendaRepository.GetById(mapVenda.Id);

            return _mapper.Map<VendaDTO>(venda);
        }
    }
}
