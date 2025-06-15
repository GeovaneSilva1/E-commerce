using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.Models
{
   
    public class Cliente
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; } 
        public string? RazaoSocial { get; set; }
        public string? Email { get; set; }

        [JsonIgnore]
        public ICollection<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        [JsonIgnore]
        public ICollection<Venda>? Vendas { get; set; }
        [JsonIgnore]
        public ICollection<Notificacao>? Notificacoes { get; set; }
    }
}
