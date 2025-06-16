using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly ITabelaPrecoService _tabelaPrecoService;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, ITabelaPrecoService tabelaPrecoService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
            _tabelaPrecoService = tabelaPrecoService;
        }

        public async Task AddProduto(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO)
        {
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            produto.TabelaPrecoId = tabelaPrecoDTO.Id;

            await _produtoRepository.Add(produto);
            produtoDTO.Id = produto.Id;
            produtoDTO.DescricaoTabelaPreco = produto.TabelaPreco.Descricao;
        }

        public async Task<ProdutoDTO> GetById(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }
    }
}
