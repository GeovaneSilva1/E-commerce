namespace LojaVirtual.ProductApi.Models
{
    public class Notificacao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public DateTime DataEnvio { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Enviado";
    }
}
