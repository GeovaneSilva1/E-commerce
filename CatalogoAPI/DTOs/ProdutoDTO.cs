using LojaVirtual.CatalogoAPI.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.CatalogoAPI.DTOs
{
    public class ProdutoDTO
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
        public Categoria? Categoria { get; set; }
        [JsonIgnore]
        public long CategoriaId { get; set; }
        [JsonIgnore]
        public Marca? Marca { get; set; }
        [JsonIgnore]
        public long MarcaId { get; set; }
        [JsonIgnore]
        public ICollection<ImagemProduto>? ImagemProdutos { get; set; }
    }
}
