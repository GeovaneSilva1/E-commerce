using LojaVirtual.CatalogoAPI.Context;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContextCatalogoApi _contextCatalogo;

        public ProdutoRepository(AppDbContextCatalogoApi contextCatalogo)
        {
            _contextCatalogo = contextCatalogo;
        }

        public async Task Add(Produto produto)
        {
            _contextCatalogo.Produtos.Add(produto);
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task Update(Produto produto)
        {
            _contextCatalogo.Entry(produto).State = EntityState.Modified;
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task<Produto> Delete(long handle)
        {
            var produto = await Get(handle);
            _contextCatalogo.Produtos.Remove(produto);
            await _contextCatalogo.SaveChangesAsync();
            return produto;
        }

        public async Task<bool> Exists(long handle)
        {
            return await _contextCatalogo.Produtos.AnyAsync(p => p.Handle == handle);
        }

        public async Task<Produto> Get(long handle)
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria )
                .Include(p => p.Marca)
                .Where(p => p.Handle == handle).FirstOrDefaultAsync();
        }

        public async Task<Produto> GetBySKU(string SKU)
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Where(p => p.SKU == SKU).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Produto>> GetMany()
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByCategoriaId(long categoriaHandle)
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Where(p => p.CategoriaId == categoriaHandle).ToListAsync();
        }
    }
}
