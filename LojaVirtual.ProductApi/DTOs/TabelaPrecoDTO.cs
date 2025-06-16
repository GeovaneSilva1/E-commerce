using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class TabelaPrecoDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Descricao { get; set; }
        [JsonIgnore]
        public DateTime DataInicio { get; set; }
        [JsonIgnore]
        public DateTime DataFim { get; set; }
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
        public int DiasValidos { get; set; }
    }
}
