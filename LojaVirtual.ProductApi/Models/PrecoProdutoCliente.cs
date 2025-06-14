using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("precoprodutoclientes")]
    public class PrecoProdutoCliente
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }

        /*public PrecoProdutoCliente(int ProdutoId, int ClienteId, int TabelaPrecoId, decimal Valor)
        {
            this.pro_Id = ProdutoId;
            this.cli_Id = ClienteId;
            this.tp_Id = TabelaPrecoId;
            this.Valor = Valor;
        }*/

        //public TabelaPreco? TabelaPreco { get; set; }
        public Produto? Produto { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
