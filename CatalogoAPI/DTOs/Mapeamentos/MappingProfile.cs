using AutoMapper;
using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.DTOs.Mapeamentos
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProdutoDTO, Produto>();
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(pdt => pdt.NomeMarca, p => p.MapFrom(src => src.Marca.Nome))
                .ForMember(pdt => pdt.NomeCategoria, p => p.MapFrom(src => src.Categoria.Nome));

            CreateMap<ImagemProdutoDTO, ImagemProduto>();
            CreateMap<ImagemProduto, ImagemProdutoDTO>()
                .ForMember(imdt => imdt.NomeProduto, im => im.MapFrom(src => src.Produto.Descricao));

            CreateMap<CategoriaDTO, Categoria>().ReverseMap();

            CreateMap<MarcaDTO, Marca>().ReverseMap();
        }
    }
}
