using AutoMapper;
using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using LojaVirtual.CatalogoAPI.Services.Interfaces;

namespace LojaVirtual.CatalogoAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task AddProduto(ProdutoDTO produtoDTO, CategoriaDTO categoriaDTO, MarcaDTO marcaDTO)
        {
            produtoDTO.Handle = 0;
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            produto.CategoriaId = categoriaDTO.Handle;
            produto.MarcaId = marcaDTO.Handle;

            await _produtoRepository.Add(produto);
            produtoDTO.Handle = produto.Handle;
            produtoDTO.NomeCategoria = produto.Categoria.Nome;
            produtoDTO.NomeMarca = produto.Marca.Nome;
            produtoDTO.PrecoPromocional = await ApplyDescontoAvistaAsync(produto);
        }

        public async Task UpdateProduto(ProdutoDTO produtoDTO, CategoriaDTO categoriaDTO, MarcaDTO marcaDTO)
        {
            Produto produto = _mapper.Map<Produto>(produtoDTO);
            produto.CategoriaId = categoriaDTO.Handle;
            produto.MarcaId = marcaDTO.Handle;
            
            await _produtoRepository.Update(produto);
            produtoDTO.Handle = produto.Handle;
        }
        
        public async Task<ProdutoDTO> DeleteProduto(long handle)
        {
            Produto produtoDeletado = await _produtoRepository.Delete(handle);
            return _mapper.Map<ProdutoDTO>(produtoDeletado);
        }

        public async Task<bool> ExistProduto(long handle)
        {
            return await _produtoRepository.Exists(handle);
        }

        public async Task<ProdutoDTO> GetBySKU(string SKU)
        {
            Produto produto = await _produtoRepository.GetBySKU(SKU);
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<ProdutoDTO> GetProduto(long handle)
        {
            Produto produto = await _produtoRepository.Get(handle);
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            if (produtoDTO is not null)
                produtoDTO.PrecoPromocional = await ApplyDescontoAvistaAsync(produto);

            return produtoDTO;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produtos = await _produtoRepository.GetMany();
            var produtoDtos = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            foreach (var produtoDto in produtoDtos.Where(pdt => pdt is not null))
            {
                var produtoAtual = produtos.FirstOrDefault(p => p.Handle == produtoDto.Handle);
                if (produtoAtual is null)
                    continue;

                produtoDto.PrecoPromocional = await ApplyDescontoAvistaAsync(produtoAtual);
            }

            return produtoDtos;
        }

        private static Task<decimal> ApplyDescontoAvistaAsync(Produto produto)
        {
            decimal precoPromocional = produto.Preco;

            if (produto.PercentualDescontoAvista.GetValueOrDefault() > 0)
            {
                precoPromocional -= produto.Preco * (produto.PercentualDescontoAvista.GetValueOrDefault() / 100);
            }

            return Task.FromResult(precoPromocional);
        }
    }
}
