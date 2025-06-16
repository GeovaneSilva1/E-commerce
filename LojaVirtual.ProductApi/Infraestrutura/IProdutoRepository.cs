using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IProdutoRepository
    {
        Task Add(Produto produto);
        public List<Produto> GetMany();
        Task<Produto> GetById(int id);
        Produto GetBySKU(string SKU);
        Produto Update(Produto produto, decimal preco);
        bool ExistById(int id);
        bool ExistBySKU(string SKU);
        decimal GetPrecoUnitarioById(int id);


    }
}
