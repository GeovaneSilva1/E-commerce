using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class CondicaoPagamentoDTO
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public int? Dias { get; set; }
        [JsonIgnore]
        public ICollection<Venda>? Vendas { get; set; }
    }
}
