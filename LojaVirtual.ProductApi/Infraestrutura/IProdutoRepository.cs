using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IProdutoRepository
    {
        void Add(Produto produto);
        Produto GetById(int id);
        Produto GetBySKU(string SKU);
        bool ExistById(int id);
        bool ExistBySKU(string SKU);
        decimal GetPrecoUnitarioById(int id);


    }
}
