using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("condicaopagamentos")]
    public class CondicaoPagamento
    {
        public int Id { get; set; }
        public string? Descricao { get; set; } //Boleto,Credito
        public string? Dias { get; set; } //30,60,90

        public List<Venda>? Vendas { get; set; }
        public CondicaoPagamento(string Descricao, string Dias)
        {
            this.Descricao = Descricao;
            this.Dias = Dias;
        }
    }
}
