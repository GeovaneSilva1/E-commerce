using System.ComponentModel.DataAnnotations.Schema;

namespace CatalogoAPI.Models
{
    [Table("categorias")]
    public class Categoria
    {
        public long Handle { get; set; }
        public string? Nome { get; set; }
        
        public ICollection<Produto>? Produtos { get; set; }
        
    }
}
