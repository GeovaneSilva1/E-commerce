using LojaVirtual.ProductApi.Models;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string? SKU { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }

        public string DescricaoTabelaPreco { get; set; }
        [JsonIgnore]
        public TabelaPreco? TabelaPreco { get; set; }
        [JsonIgnore]
        public ICollection<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        [JsonIgnore]
        public ICollection<VendaItem>? VendaItem { get; set; }
        [JsonIgnore]
        public ICollection<Notificacao>? Notificacoes { get; set; }
    }
}
