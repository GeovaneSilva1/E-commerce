namespace LojaVirtual.ProductApi.Models
{
    public class PrecoProdutoCliente
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int ClienteId { get; set; }
        public int TabelaPrecoId { get; set; }
        public decimal Valor { get; set; }
    }
}
