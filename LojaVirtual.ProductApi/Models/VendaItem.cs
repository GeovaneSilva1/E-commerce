using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("vendaitens")]
    public class VendaItem
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        public VendaItem(int VendaId, int ProdutoId, int Quantidade, decimal ValorUnitario)
        {
            this.VendaId = VendaId;
            this.ProdutoId = ProdutoId;
            this.Quantidade = Quantidade;
            this.ValorUnitario = ValorUnitario;
        }
    }
}
