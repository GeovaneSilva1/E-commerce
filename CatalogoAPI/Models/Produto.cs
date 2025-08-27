using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.CatalogoAPI.Models
{
    [Table("produtos")]
    public class Produto
    {
        public long Handle { get; set; }
        public string? Descricao { get; set; }
        public string? SKU { get; set; }
        public decimal Preco { get; set; }
        public decimal? PercentualDescontoAvista { get; set; }
        public long Estoque { get; set; }
        public Categoria? Categoria { get; set; }
        public long? CategoriaId { get; set; }
        public Marca? Marca { get; set; }
        public long? MarcaId { get; set; }

        public ICollection<ImagemProduto>? ImagemProdutos { get; set; }
    }
}
