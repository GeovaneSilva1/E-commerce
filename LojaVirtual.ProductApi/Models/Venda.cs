using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("vendas")]
    public class Venda
    {
        public int Id { get; set; }
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public CondicaoPagamento? CondicaoPagamento { get; set; }
        public int CondicaoPagamentoId { get; set; }

        public ICollection<VendaItem> VendaItem { get; set; }
    }
}
