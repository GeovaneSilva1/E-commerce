using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LojaVirtual.Web.Models
{
    public class ProdutoViewModel
    {
        public long Handle { get; set; }
        [JsonIgnore]
        public string? SKU { get; set; }
        [Required(ErrorMessage = "A descrição do produto é obrigatória!")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O preço é obrigatório!")]
        public decimal Preco { get; set; }
        [Required(ErrorMessage = "O Estoque é obrigatório!")]
        public long Estoque { get; set; }
        [DisplayName("Categoria")]
        public string? NomeCategoria { get; set; }
        [DisplayName("Marca")]
        public string? NomeMarca { get; set; }
        [DisplayName("Categoria")]
        public long CategoriaId { get; set; }
        [DisplayName("Marca")]
        public long MarcaId { get; set; }
    }
}
