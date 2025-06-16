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

        public List<Produto> GetMany()
        {
            return _appDbContext.Produtos.ToList();
        }

        public bool ExistById(int id)
        {
            return _appDbContext.Produtos.Any(p => p.Id == id);
        }

        public bool ExistBySKU(string SKU)
        {
            return _appDbContext.Produtos.Any(p => p.SKU == SKU);
        }

        public async Task<Produto> GetById(int id)
        {
            return await _appDbContext.Produtos.Include(p => p.TabelaPreco).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public Produto GetBySKU(string SKU)
        {
            return _appDbContext.Produtos.Where(p => p.SKU == SKU).FirstOrDefault();
        }

        public decimal GetPrecoUnitarioById(int id)
        {
            return _appDbContext.Produtos.Where(p => p.Id == id).Select(pc => pc.Preco).FirstOrDefault();
        }

        public Produto Update(Produto produto, decimal preco)
        {
            _appDbContext.Entry(produto).State = EntityState.Modified;
            produto.Preco = preco;
            _appDbContext.SaveChanges();
            return produto;
        }
    }
}
