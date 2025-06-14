using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models;

[Table("produtos")]
public class Produto
{
    public int Id { get; set; }
    public string? SKU { get; set; }
    public string? Descricao { get; set; }
    public decimal Preco { get; set; }
    public TabelaPreco? TabelaPreco { get; set; }
    public ICollection<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
    public ICollection<VendaItem>? VendaItem { get; set; }
    public ICollection<Notificacao>? Notificacoes { get; set; }
    

    
}
