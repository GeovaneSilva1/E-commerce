using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("vendaitens")]
    public class VendaItem
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public Venda? Venda { get; set; }
        public int VendaId { get; set; }
        public Produto? Produto { get; set; }
        public int ProdutoId { get; set; }
    }
}
