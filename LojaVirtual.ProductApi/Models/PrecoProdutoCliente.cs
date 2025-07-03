using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("precoprodutoclientes")]
    public class PrecoProdutoCliente
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Produto? Produto { get; set; }
        public int ProdutoId { get; set; }
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
    }
}
