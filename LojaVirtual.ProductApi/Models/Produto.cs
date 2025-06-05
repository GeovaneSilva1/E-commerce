using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models;

[Table("produtos")]
public class Produto
{
    public int Id { get; set; }
    public string? SKU { get; set; }
    public string? Descricao { get; set; }

    //public List<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
    //public List<VendaItem>? VendaItems { get; set; }
    //public List<Notificacao>? Notificacoes { get; set; }
    public Produto(int Id, string SKU, string Descricao)
    {
        this.Id = Id;
        this.SKU = SKU;
        this.Descricao = Descricao;
    }
}
