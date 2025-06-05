namespace LojaVirtual.ProductApi.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public int CondicaoPagamentoId { get; set; }

        public Venda(int ClienteId, int CondicaoPagamentoId)
        {
            this.ClienteId = ClienteId;
            this.CondicaoPagamentoId = CondicaoPagamentoId;
        }
        public List<VendaItem> Itens { get; set; } = new();
    }
}
