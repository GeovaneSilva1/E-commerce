using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class VendaItemDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? ProdutoDescricao { get; set; }
        public int Quantidade { get; set; }
        [JsonIgnore]
        public decimal Valor { get; set; }
        [JsonIgnore]
        public Venda? Venda { get; set; }
        [JsonIgnore]
        public int VendaId { get; set; }
        [JsonIgnore]
        public Produto? Produto { get; set; }
        [JsonIgnore]
        public int ProdutoId { get; set; }
    }
}
