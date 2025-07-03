using LojaVirtual.ProductApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.DTOs
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O CNPJ/CPF é Obrigatório!")]
        [MinLength(3)]
        [MaxLength(18)]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "O Nome é Obrigatório!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? RazaoSocial { get; set; }

        [Required(ErrorMessage = "O Email é Obrigatório!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string? Email { get; set; }

        [JsonIgnore]
        public ICollection<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        [JsonIgnore]
        public ICollection<Venda>? Vendas { get; set; }
        [JsonIgnore]
        public ICollection<Notificacao>? Notificacoes { get; set; }
    }
}
