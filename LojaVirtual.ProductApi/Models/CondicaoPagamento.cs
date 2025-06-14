using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("condicaopagamentos")]
    public class CondicaoPagamento
    {
        public int Id { get; set; }
        public string? Descricao { get; set; } 
        public int? Dias { get; set; }

        public CondicaoPagamento(string Descricao, int? Dias)
        {
            this.Descricao = Descricao;
            this.Dias = Dias;
        }
        public ICollection<Venda>? Vendas { get; set; }
    }
}
