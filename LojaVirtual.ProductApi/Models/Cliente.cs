using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("clientes")]
    public class Cliente
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; } 
        public string? RazaoSocial { get; set; }

        public Cliente(string CNPJ, string RazaoSocial) 
        { 
            this.CNPJ = CNPJ;
            this.RazaoSocial = RazaoSocial;
        }
    }
}
