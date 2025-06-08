using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.ProductApi.Models
{
    [Table("tabelaprecos")]
    public class TabelaPreco
    {
        public int Id { get; set; }
        public string? Descricao { get; set; } 
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public TabelaPreco(string Descricao, DateTime DataInicio, DateTime DataFim) 
        {
            this.Descricao = Descricao;
            this.DataInicio = DataInicio;
            this.DataFim = DataFim;
        }

        public List<PrecoProdutoCliente>? PrecoProdutoClientes { get; set; }
        public List<Produto>? Produtos { get; set; }
    }
}
