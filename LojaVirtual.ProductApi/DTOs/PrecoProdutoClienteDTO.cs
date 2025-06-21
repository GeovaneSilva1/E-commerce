using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.DTOs
{
    public class PrecoProdutoClienteDTO
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Produto? Produto { get; set; }
        public int ProdutoId { get; set; }
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
    }
}
