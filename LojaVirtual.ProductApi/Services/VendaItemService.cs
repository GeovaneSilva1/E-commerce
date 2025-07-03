using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class VendaItemService : IVendaItemService
    {
        private readonly IVendaItemRepository _vendaItemRepository;
        private readonly IMapper _mapper;

        public VendaItemService(IVendaItemRepository vendaItemRepository, IMapper mapper)
        {
            _vendaItemRepository = vendaItemRepository;
            _mapper = mapper;
        }

        public async Task AddVendaItens(IEnumerable<VendaItemDTO> itensDTO)
        {
            foreach (var itemDTO in itensDTO)
            {
                VendaItem vendaItem = _mapper.Map<VendaItem>(itemDTO);
                await _vendaItemRepository.Add(vendaItem);
            }
        }

        public async Task<decimal> GetValorTotalVenda(int id)
        {
            return await _vendaItemRepository.GetValorTotalVenda(id);
        }
    }
}
