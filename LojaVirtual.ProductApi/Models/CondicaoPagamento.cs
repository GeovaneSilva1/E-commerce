namespace LojaVirtual.ProductApi.Models
{
    public class CondicaoPagamento
    {
        public int Id { get; set; }
        public string? Descricao { get; set; }
        public string? Dias { get; set; }

        public List<Venda>? Vendas { get; set; }
    }
}
