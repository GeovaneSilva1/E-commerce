using LojaVirtual.ProductApi.DTOs;

namespace LojaVirtual.ProductApi.Services
{
    public interface ICondicaoPagamentoService
    {
        Task AddCondicaoPagamento(CondicaoPagamentoDTO? condicaoPagamentoDTO);
        Task<IEnumerable<CondicaoPagamentoDTO>> GetCondicoesPagamento();
        Task<CondicaoPagamentoDTO> GetCondPagamentoByDescAndDias(string? descricao, int? dias);
    }
}
