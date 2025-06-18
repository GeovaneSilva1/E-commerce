using LojaVirtual.ProductApi.Models;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.ProductApi.DTOs
{
    public class CompraDTO
    {
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
        [Required(ErrorMessage = "A Condição de pagamento é Obrigatória!")]
        public CondicaoPagamentoDTO? CondicaoPagamentoDTO { get; set; }
        [Required(ErrorMessage = "O nome e quantidade de produto é Obrigatória!")]
        public IEnumerable<VendaItemDTO>? ItensDTO { get; set; }
    }
    /*public class ItemCompra
    {
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public int Quantidade { get; set; }
    }

    public class AtributosCondicaoPagamento
    {
        public string? Descricao { get; set; }
        public int? Dias { get; set; }
    } */
}
