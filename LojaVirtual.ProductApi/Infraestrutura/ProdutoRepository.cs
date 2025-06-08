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

        public void Add(Produto produto)
        {
            _appDbContext.Produtos.Add(produto);
            _appDbContext.SaveChanges();
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

        public Produto GetById(int id)
        {
            return _appDbContext.Produtos.Where(p => p.Id == id).FirstOrDefault();
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
