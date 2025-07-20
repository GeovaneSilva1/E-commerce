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
            return _mapper.Map<ProdutoDTO>(produto);
        }

        public async Task<IEnumerable<ProdutoDTO>> GetProdutos()
        {
            var produto = await _produtoRepository.GetMany();
            return _mapper.Map<IEnumerable<ProdutoDTO>>(produto);
        }
    }
}
