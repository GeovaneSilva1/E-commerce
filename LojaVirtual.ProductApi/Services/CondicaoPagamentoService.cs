using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class CondicaoPagamentoService : ICondicaoPagamentoService
    {
        private readonly ICondicaoPagamentoRepository _condicaoPagamentoRepository;
        private readonly IMapper _mapper;

        public CondicaoPagamentoService(ICondicaoPagamentoRepository condicaoPagamentoRepository, IMapper mapper)
        {
            _condicaoPagamentoRepository = condicaoPagamentoRepository;
            _mapper = mapper;
        }

        public async Task AddCondicaoPagamento(CondicaoPagamentoDTO? condicaoPagamentoDTO)
        {
            condicaoPagamentoDTO.Id = 0;
            CondicaoPagamento condicaoPagamento = _mapper.Map<CondicaoPagamento>(condicaoPagamentoDTO);
            await _condicaoPagamentoRepository.Add(condicaoPagamento);
            condicaoPagamentoDTO.Id = condicaoPagamento.Id;
        }

        public async Task<IEnumerable<CondicaoPagamentoDTO>> GetCondicoesPagamento()
        {
            IEnumerable<CondicaoPagamento> condicaoPagamentos = await _condicaoPagamentoRepository.GetMany();
            return _mapper.Map<IEnumerable<CondicaoPagamentoDTO>>(condicaoPagamentos);
        }

        public async Task<CondicaoPagamentoDTO> GetCondPagamentoByDescAndDias(string? descricao, int? dias)
        {
            CondicaoPagamento condicaoPagamento = await _condicaoPagamentoRepository.GetByDescAndDias(descricao, dias);
            return _mapper.Map<CondicaoPagamentoDTO>(condicaoPagamento);
        }
    }
}
