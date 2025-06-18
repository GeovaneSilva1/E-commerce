using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.DTOs.Mapeamentos
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClienteDTO, Cliente>();
            CreateMap<Cliente, ClienteDTO>();

            CreateMap<ProdutoDTO, Produto>();
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(pdt => pdt.DescricaoTabelaPreco, p => p.MapFrom(src => src.TabelaPreco.Descricao));

            CreateMap<TabelaPrecoDTO, TabelaPreco>();
            CreateMap<TabelaPreco, TabelaPrecoDTO>();

            CreateMap<TabelaPrecoResponseDTO, TabelaPreco>();
            CreateMap<TabelaPreco, TabelaPrecoResponseDTO>();

            CreateMap<CondicaoPagamentoDTO, CondicaoPagamento>();
            CreateMap<CondicaoPagamento, CondicaoPagamentoDTO>();

            CreateMap<ClienteDTO,CompraDTO>();
            CreateMap<CompraDTO, ClienteDTO>();

            CreateMap<VendaDTO, Venda>();
            CreateMap<Venda, VendaDTO>();

            CreateMap<VendaItemDTO, VendaItem>();
            CreateMap<VendaItem, VendaItemDTO>();

            CreateMap<VendaItemResponseDTO, VendaItemDTO>();
            CreateMap<VendaItemDTO, VendaItemResponseDTO>();
        }
    }
}
