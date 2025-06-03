namespace LojaVirtual.ProductApi.Models;

public class Produto
{
    public int Id { get; set; }
    public string? SKU { get; set; }
    public string? Descricao { get; set; }

    public List<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
    public List<VendaItem>? VendaItems { get; set; }
    public List<Notificacao>? Notificacoes { get; set; }
}
