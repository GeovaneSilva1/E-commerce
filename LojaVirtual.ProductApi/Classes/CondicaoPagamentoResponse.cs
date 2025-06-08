using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Classes
{
    public class CondicaoPagamentoResponse
    {
        public List<AtributosCondicao>? condicaoPagamentos { get; set; }

        public void IncluirAtributos(List<CondicaoPagamento> condicaoPagamento)
        {
            this.condicaoPagamentos = new List<AtributosCondicao>();

            for (int count = 0; count < condicaoPagamento.Count; count++)
            {
                this.condicaoPagamentos.Add(new AtributosCondicao
                {
                    Id = condicaoPagamento[count].Id,
                    Descricao = condicaoPagamento[count].Descricao,
                    Dias = condicaoPagamento [count].Dias
                });
            }
        }
    }

    public class AtributosCondicao
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public string? Dias { get; set; }

    }
}
