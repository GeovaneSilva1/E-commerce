namespace LojaVirtual.ProductApi.Models
{
    public class TabelaPreco
    {
        public int Id { get; set; }
        public string? Descricao { get; set; } 
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }

        public List<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}
