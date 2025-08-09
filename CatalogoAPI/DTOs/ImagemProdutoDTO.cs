using LojaVirtual.CatalogoAPI.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.CatalogoAPI.DTOs
{
    public class ImagemProdutoDTO
    {
        public long Handle { get; set; }
        public string? Url { get; set; }
        public string? NomeProduto { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; }
        public long? ProdutoId { get; set; }
    }
}
