using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface ICondicaoPagamentoRepository
    {
        Task Add(CondicaoPagamento condicaoPagamento);
        Task<IEnumerable<CondicaoPagamento>> GetMany();
        CondicaoPagamento GetById(int id);
        Task<CondicaoPagamento> GetByDescAndDias(string descricao, int? dias);
        bool ExistById(int id);
        bool ExistByDescAndDias(string descricao, int? dias);
    }
}
