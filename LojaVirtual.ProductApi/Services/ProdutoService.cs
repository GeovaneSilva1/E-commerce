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

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper, ITabelaPrecoService tabelaPrecoService)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task AddProduto(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO)
        {
            produtoDTO.Id = 0;
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            produto.TabelaPrecoId = tabelaPrecoDTO.Id;

            await _produtoRepository.Add(produto);
            produtoDTO.Id = produto.Id;
            produtoDTO.DescricaoTabelaPreco = produto.TabelaPreco.Descricao;
        }

        public async Task<bool> ExistProdutoById(int id)
        {
            return await _produtoRepository.ExistById(id);
        }

        public async Task<ProdutoDTO> GetById(int id)
        {
            var produto = await _produtoRepository.GetById(id);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produto = await _produtoRepository.GetMany();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produto);
        }

        public async Task UpdateProdutoById(ProdutoDTO produtoDTO, TabelaPrecoDTO tabelaPrecoDTO)
        {
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            produto.TabelaPrecoId = tabelaPrecoDTO.Id;
            await _produtoRepository.Update(produto);
            produtoDTO.Id = produto.Id;
        }

        public string MontaSKUByNome(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return string.Empty;

            var palavras = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (palavras.Length < 3)
                return string.Empty;

            string primeira = palavras[0].Length >= 3 ? palavras[0].Substring(0, 3) : palavras[0];
            string segunda = palavras[1].Length >= 2 ? palavras[1].Substring(0, 2) : palavras[1];
            string terceira = palavras[2].Substring(0, 1);

            return $"{primeira.ToUpper()}-{segunda.ToUpper()}-{terceira.ToUpper()}";

        }

        public async Task<Produto> GetBySKU(string SKU)
        {
            return await _produtoRepository.GetBySKU(SKU);
        }
    }
}
