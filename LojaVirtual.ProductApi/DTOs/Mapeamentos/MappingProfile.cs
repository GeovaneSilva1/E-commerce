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

            //CreateMap<IEnumerable<TabelaPrecoDTO>,IEnumerable<TabelaPreco>>();
            //CreateMap<IEnumerable<TabelaPreco>, IEnumerable<TabelaPrecoDTO>>();

            CreateMap<TabelaPrecoResponseDTO, TabelaPreco>();
            CreateMap<TabelaPreco, TabelaPrecoResponseDTO>();

            //CreateMap<IEnumerable<TabelaPrecoResponseDTO>, IEnumerable<TabelaPreco>>();
            //CreateMap<IEnumerable<TabelaPreco>, IEnumerable<TabelaPrecoResponseDTO>>();
        }
    }
}
