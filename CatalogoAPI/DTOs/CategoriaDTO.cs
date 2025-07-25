using LojaVirtual.CatalogoAPI.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.CatalogoAPI.DTOs
{
    public class CategoriaDTO
    {
        public long Handle { get; set; }
        public string? Nome { get; set; }
        
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
