using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class VendaDTO
    {
        public int Id { get; set; }
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
        public CondicaoPagamento? CondicaoPagamento { get; set; }
        public int CondicaoPagamentoId { get; set; }
        [JsonIgnore]
        public string? ErroVenda { get; set; }

        [JsonIgnore]
        public ICollection<VendaItem> VendaItem { get; set; }
    }
}
