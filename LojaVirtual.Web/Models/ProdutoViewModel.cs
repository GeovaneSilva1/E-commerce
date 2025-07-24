using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LojaVirtual.Web.Models
{
    public class ProdutoViewModel
    {
        public long Handle { get; set; }
        [JsonIgnore]
        public string? SKU { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public long Estoque { get; set; }
        public string? NomeCategoria { get; set; }
        public string? NomeMarca { get; set; }

        [JsonIgnore]
        public long CategoriaId { get; set; }
        [JsonIgnore]
        public long MarcaId { get; set; }
    }
}
