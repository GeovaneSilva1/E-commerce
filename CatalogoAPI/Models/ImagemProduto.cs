using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models
{
    [Table("imagensprodutos")]
    public class ImagemProduto
    {
        public long Handle { get; set; }
        public string? Url { get; set; }
        public Produto? Produto { get; set; }
        public long? ProdutoId { get; set; }
    }
}
