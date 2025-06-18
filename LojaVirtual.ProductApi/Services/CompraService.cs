using AutoMapper;
using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Services
{
    public class CompraService : ICompraService
    {
        private readonly IClienteService _clienteService;
        private readonly ICondicaoPagamentoService _condicaoPagamentoService;
        private readonly IVendaService _vendaService;
        private readonly IMapper _mapper;
        private readonly IProdutoService _produtoService;
        private readonly IVendaItemService _vendaItemService;
        private readonly AppDbContext _appDbContext;

        public CompraService(IClienteService clienteService, 
                             ICondicaoPagamentoService condicaoPagamentoService, 
                             IVendaService vendaService, 
                             IMapper mapper,
                             IProdutoService produtoService,
                             IVendaItemService vendaItemService,
                             AppDbContext appDbContext)
        {
            _clienteService = clienteService;
            _condicaoPagamentoService = condicaoPagamentoService;
            _vendaService = vendaService;
            _produtoService = produtoService;
            _vendaItemService = vendaItemService;
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<ClienteDTO> RetornaClienteDTO(CompraDTO compra)
        {
            ClienteDTO clienteDTO = await _clienteService.GetByCNPJ(compra.CNPJ);
            if (clienteDTO is null)
            {
                clienteDTO = _mapper.Map<ClienteDTO>(compra);
                await _clienteService.AddCliente(clienteDTO);
            }

            return clienteDTO;
        }

        public async Task<CondicaoPagamentoDTO> RetornaCondicaoPagamentoDTO(CompraDTO compra)
        {
            CondicaoPagamentoDTO condicaoPagamentoDTO = await _condicaoPagamentoService.GetCondPagamentoByDescAndDias(compra.CondicaoPagamentoDTO.Descricao, compra.CondicaoPagamentoDTO.Dias);
            if (condicaoPagamentoDTO is null)
            {
                condicaoPagamentoDTO = compra.CondicaoPagamentoDTO;
                await _condicaoPagamentoService.AddCondicaoPagamento(condicaoPagamentoDTO);
            }
            
            return condicaoPagamentoDTO;
        }

        public async Task<VendaDTO> RetornaVendaDTO(ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            VendaDTO vendaDTO = await _vendaService.AddVenda(clienteDTO, condicaoPagamentoDTO);
            return vendaDTO;
        }

        public async Task<VendaItemResponseDTO> GetVendaItensResponseDTO(VendaDTO vendaDTO, IEnumerable<VendaItemDTO>? itensDTO)
        {
            decimal valorTotalVenda = await _vendaItemService.GetValorTotalVenda(vendaDTO.Id);
            VendaItemResponseDTO vendaItemResponseDTO = new VendaItemResponseDTO();
            vendaItemResponseDTO.VendaItensDTO = itensDTO;
            vendaItemResponseDTO.valorTotalCompra = valorTotalVenda;
            return vendaItemResponseDTO;
        }

        public async Task<VendaDTO> CriaVendaItensVenda(IEnumerable<VendaItemDTO>? itensDTO, ClienteDTO clienteDTO, CondicaoPagamentoDTO condicaoPagamentoDTO)
        {
            using var transaction = await _appDbContext.Database.BeginTransactionAsync();
            try
            {
                VendaDTO vendaDTO = await RetornaVendaDTO(clienteDTO, condicaoPagamentoDTO);

                foreach (var vendaItemDTO in itensDTO)
                {
                    if (vendaItemDTO.Quantidade <= 0)
                    {
                        vendaDTO.ErroVenda = $"Quantidade do produto {vendaItemDTO.ProdutoDescricao} não pode ser zero!";
                        return vendaDTO;
                    }

                    string SKU = _produtoService.MontaSKUByNome(vendaItemDTO.ProdutoDescricao);
                    if (SKU.Equals(""))
                    {
                        vendaDTO.ErroVenda = $"Nome do produto {vendaItemDTO.ProdutoDescricao} inválido! Ex: nome cor tamanho";
                        return vendaDTO;
                    }

                    var produto = await _produtoService.GetBySKU(SKU);
                    if (produto is null)
                    {
                        vendaDTO.ErroVenda = $"Produto {vendaItemDTO.ProdutoDescricao} não encontrado!";
                        return vendaDTO;
                    }
                    vendaItemDTO.VendaId = vendaDTO.Id;
                    vendaItemDTO.ProdutoId = produto.Id;
                    vendaItemDTO.Valor = (vendaItemDTO.Quantidade * produto.Preco);
                }

                await _vendaItemService.AddVendaItens(itensDTO);
                await _appDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return vendaDTO;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
