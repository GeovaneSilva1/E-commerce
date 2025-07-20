using LojaVirtual.CatalogoAPI.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.CatalogoAPI.DTOs
{
    public class ImagemProdutoDTO
    {
        [JsonIgnore]
        public long Handle { get; set; }
        public string? Url { get; set; }
        public string? NomeProduto { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; }
        [JsonIgnore]
        public long? ProdutoId { get; set; }
    }
}
