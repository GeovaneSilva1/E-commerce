namespace LojaVirtual.ProductApi.Models
{
    public class PrecoProdutoCliente
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ClienteId { get; set; }
        public int TabelaPrecoId { get; set; }
        public decimal Valor { get; set; }

        public PrecoProdutoCliente(int ProdutoId, int ClienteId, int TabelaPrecoId, decimal Valor)
        {
            this.ProdutoId = ProdutoId;
            this.ClienteId = ClienteId;
            this.TabelaPrecoId = TabelaPrecoId;
            this.Valor = Valor;
        }
    }
}
