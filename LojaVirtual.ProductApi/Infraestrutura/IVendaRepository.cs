using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IVendaRepository
    {
        void Add(Venda venda);
        List<VendaRelatorio> GetQueryRelatorioVendas(string CNPJ, string razaoSocial);
    }
}
