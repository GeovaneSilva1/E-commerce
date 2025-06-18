using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface IVendaRepository
    {
        Task Add(Venda venda);
        Task<Venda> GetById(int id);
        List<VendaRelatorio> GetQueryRelatorioVendas(string CNPJ, string razaoSocial);
    }
}
