using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("vendas")]
    public class Venda
    {
        public int Id { get; set; }
        public Cliente? Cliente { get; set; }
        public DateTime Data { get; set; }
        public CondicaoPagamento? CondicaoPagamento { get; set; }

        /*public Venda(int ClienteId, int CondicaoPagamentoId)
        {
            this.ClienteId = ClienteId;
            //this.CondicaoPagamentoId = CondicaoPagamentoId;
            this.Data = DateTime.Now;
        } */
        

        public ICollection<VendaItem> VendaItem { get; set; }
    }
}
