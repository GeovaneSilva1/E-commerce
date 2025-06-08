using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; } 
        public string RazaoSocial { get; set; }
        public string? Email { get; set; }

        public List<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        public List<Venda>? Vendas { get; set; }
        public List<Notificacao>? Notificacoes { get; set; }

        public Cliente(string CNPJ, string RazaoSocial, string Email) 
        { 
            this.CNPJ = CNPJ;
            this.RazaoSocial = RazaoSocial;
            this.Email = Email;
        }
    }
}
