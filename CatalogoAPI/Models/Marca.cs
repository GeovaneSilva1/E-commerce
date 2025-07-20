using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models
{
    [Table("marcas")]
    public class Marca
    {
        public long Handle { get; set; }
        public string? Nome { get; set; }
        
        public ICollection<Produto>? Produtos { get; set; }
    }
}
