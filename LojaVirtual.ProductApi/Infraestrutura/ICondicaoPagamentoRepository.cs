using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface ICondicaoPagamentoRepository
    {
        Task Add(CondicaoPagamento condicaoPagamento);
        Task<IEnumerable<CondicaoPagamento>> GetMany();
        Task<CondicaoPagamento> GetByDescAndDias(string descricao, int? dias);
    }
}
