namespace LojaVirtual.ProductApi.Classes
{
    public class VendaRelatorio
    {
        public int IdVenda { get; set; }
        public string? NomeCliente { get; set; }
        public string? Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
