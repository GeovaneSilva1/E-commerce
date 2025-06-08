using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public interface ICondicaoPagamentoRepository
    {
        void Add(CondicaoPagamento condicaoPagamento);
        public List<CondicaoPagamento> GetMany();
        CondicaoPagamento GetById(int id);
        CondicaoPagamento GetByByDescAndDias(string descricao, string dias);
        bool ExistById(int id);
        bool ExistByDescAndDias(string descricao, string dias);
    }
}
