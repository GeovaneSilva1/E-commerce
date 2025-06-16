using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProdutoRepository(AppDbContext context)
        {
            _appDbContext = context;
        }

        public async Task Add(Produto produto)
        {
            _appDbContext.Produtos.Add(produto);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetMany()
        {
            return await _appDbContext.Produtos.Include(p => p.TabelaPreco).ToListAsync();
        }

        public async Task Update(Produto produto)
        {
            _appDbContext.Entry(produto).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistById(int id)
        {
            return await _appDbContext.Produtos.AnyAsync(p => p.Id == id);
        }

        public async Task<Produto> GetById(int id)
        {
            return await _appDbContext.Produtos.Include(p => p.TabelaPreco).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public bool ExistBySKU(string SKU)
        {
            return _appDbContext.Produtos.Any(p => p.SKU == SKU);
        }

        public Produto GetBySKU(string SKU)
        {
            return _appDbContext.Produtos.Where(p => p.SKU == SKU).FirstOrDefault();
        }

        public decimal GetPrecoUnitarioById(int id)
        {
            return _appDbContext.Produtos.Where(p => p.Id == id).Select(pc => pc.Preco).FirstOrDefault();
        }

        
    }
}
