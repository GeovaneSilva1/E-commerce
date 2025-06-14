using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Classes
{
    public class Compra
    {
        public string CpfCnpj { get; set; }
        public string NomeRazao { get; set; }
        public string Email { get; set; }
        public AtributosCondicaoPagamento? CondicaoPagamento { get; set; }
        public List<ItemCompra>? Itens { get; set; }
    }
    public class ItemCompra
    {
        public int ProdutoId { get; set; }
        public string? NomeProduto { get; set; }
        public int Quantidade { get; set; }
    }

    public class AtributosCondicaoPagamento
    {
        public string? Descricao { get; set; }
        public int? Dias { get; set; }
    }
}
