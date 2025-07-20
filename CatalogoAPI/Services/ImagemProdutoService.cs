using AutoMapper;
using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using LojaVirtual.CatalogoAPI.Services.Interfaces;

namespace LojaVirtual.CatalogoAPI.Services
{
    public class ImagemProdutoService : IImagemProdutoService
    {
        private readonly IImagemProdutoRepository _imagemProdutoRepository;
        private readonly IMapper _mapper;

        public ImagemProdutoService(IImagemProdutoRepository imagemProdutoRepository, IMapper mapper)
        {
            _imagemProdutoRepository = imagemProdutoRepository;
            _mapper = mapper;
        }

        public async Task AddImagemProduto(ImagemProdutoDTO imagemProdutoDTO, ProdutoDTO produtoDTO)
        {
            imagemProdutoDTO.Handle = 0;
            ImagemProduto imagemProduto = _mapper.Map<ImagemProduto>(imagemProdutoDTO);
            imagemProduto.ProdutoId = produtoDTO.Handle;

            await _imagemProdutoRepository.Add(imagemProduto);
            imagemProdutoDTO.Handle = imagemProduto.Handle;
            imagemProdutoDTO.NomeProduto = imagemProduto.Produto.Descricao;
        }

        public async Task UpdateImagemProduto(ImagemProdutoDTO imagemProdutoDTO, ProdutoDTO produtoDTO)
        {
            ImagemProduto imagemProduto = _mapper.Map<ImagemProduto>(imagemProdutoDTO);
            imagemProduto.ProdutoId = produtoDTO.Handle;    

            await _imagemProdutoRepository.Update(imagemProduto);
            imagemProdutoDTO.Handle = imagemProduto.Handle;
        }

        public async Task<ImagemProdutoDTO> DeleteImagemProduto(long handle)
        {
            ImagemProduto imagemProdutoDeletado = await _imagemProdutoRepository.Delete(handle);
            return _mapper.Map<ImagemProdutoDTO>(imagemProdutoDeletado);
        }

        public async Task<IEnumerable<ImagemProdutoDTO>> DeleteImagensProdutoByProdutoHandle(long produtoHandle)
        {
            var imagensDeletadas = await _imagemProdutoRepository.DeleteByProdutoHandle(produtoHandle);
            return _mapper.Map<IEnumerable<ImagemProdutoDTO>>(imagensDeletadas);
        }

        public async Task<IEnumerable<ImagemProdutoDTO>> GetImagensProdutoByProdutoHandle(long produtoHandle)
        {
            var imagens = await _imagemProdutoRepository.GetByProdutoHandle(produtoHandle);
            return _mapper.Map<IEnumerable<ImagemProdutoDTO>>(imagens);
        }

        public async Task<ImagemProdutoDTO> GetImagemProduto(long handle)
        {
            var imagemProduto = await _imagemProdutoRepository.Get(handle);
            return _mapper.Map<ImagemProdutoDTO>(imagemProduto);
        }

        public async Task<IEnumerable<ImagemProdutoDTO>> GetImagensProdutos()
        {
            var imagens = await _imagemProdutoRepository.GetMany();
            return _mapper.Map<IEnumerable<ImagemProdutoDTO>>(imagens);
        }

        
    }
}
