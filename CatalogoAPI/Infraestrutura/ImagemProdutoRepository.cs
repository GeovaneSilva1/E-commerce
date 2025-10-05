using AutoMapper;
using LojaVirtual.CatalogoAPI.Context;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class ImagemProdutoRepository : IImagemProdutoRepository
    {
        private readonly AppDbContextCatalogoApi _contextCatalogo;
        private readonly IMapper _mapper;

        public ImagemProdutoRepository(AppDbContextCatalogoApi contextCatalogo, IMapper mapper)
        {
            _contextCatalogo = contextCatalogo;
            _mapper = mapper;
        }

        public async Task Add(ImagemProduto imagemProduto)
        {
            _contextCatalogo.ImagensProdutos.Add(imagemProduto);
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task Update(ImagemProduto imagemProduto)
        {
            _contextCatalogo.Entry(imagemProduto).State = EntityState.Modified;
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task<ImagemProduto> Delete(long handle)
        {
            var imagemProduto = await Get(handle);
            _contextCatalogo.ImagensProdutos.Remove(imagemProduto);
            await _contextCatalogo.SaveChangesAsync();
            return imagemProduto;
        }

        public async Task<IEnumerable<ImagemProduto>> DeleteByProdutoHandle(long produtoHandle)
        {
            var imagensProduto = await GetByProdutoHandle(produtoHandle);
            _contextCatalogo.ImagensProdutos.RemoveRange(imagensProduto);
            await _contextCatalogo.SaveChangesAsync();
            return imagensProduto;
        }

        public async Task<ImagemProduto> Get(long handle)
        {
            return await _contextCatalogo.ImagensProdutos
                .Include(ip => ip.Produto)
                .Where(p => p.Handle == handle).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ImagemProduto>> GetByProdutoHandle(long produtoHandle)
        {
            return await _contextCatalogo.ImagensProdutos
                .Where(ip => ip.Produto.Handle == produtoHandle)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImagemProduto>> GetMany()
        {
            return await _contextCatalogo.ImagensProdutos
                .Include(ip => ip.Produto)
                .ToListAsync();
        }
    }
}
