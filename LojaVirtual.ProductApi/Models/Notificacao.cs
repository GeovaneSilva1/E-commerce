using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("notificacoes")]
    public class Notificacao
    {
        public int Id { get; set; }
        public string? Mensagem { get; set; }
        public DateTime DataEnvio { get; set; } = DateTime.Now;
        public string? Status { get; set; }
        public Cliente? Cliente { get; set; }
        public int ClienteId { get; set; }
        public Produto? Produto { get; set; }
        public int ProdutoId { get; set; }
    }
}
