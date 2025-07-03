using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class TabelaPrecoResponseDTO
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        [JsonIgnore]
        public ICollection<Produto>? Produtos { get; set; }
    }
}
