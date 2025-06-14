using LojaVirtual.ProductApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.ProductApi.Classes
{
    public class ClienteDTO
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O CPF/CNPJ é Obrigatório!")]
        [MinLength(0)]
        [MaxLength(18)]
        public string? CNPJ { get; set; }

        [Required(ErrorMessage = "A razão social é Obrigatória!")]
        [MinLength(3)]
        [MaxLength(100)]
        public string RazaoSocial { get; set; }
        
        [Required(ErrorMessage = "O Email é Obrigatória!")]
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
