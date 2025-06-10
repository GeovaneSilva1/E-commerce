using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Classes
{
    public class ProdutoResponse
    {
        public List<AtributosProduto>? produtos { get; set; }
        public AtributosProduto? produto { get; set; }

        public void IncluirAtributos(List<Produto> produto)
        {
            this.produtos = new List<AtributosProduto>();
            
            for (int count = 0; count < produto.Count; count++)
            {
                 this.produtos.Add(new AtributosProduto 
                 { 
                     Id = produto[count].Id,
                     SKU = produto[count].SKU,
                     Descricao = produto[count].Descricao,
                     Preco = produto[count].Preco
                 });
            }
        }

        public void IncluirAtributos(Produto produtoAlterado)
        {
            this.produto = new AtributosProduto();
            this.produto.Id  = produtoAlterado.Id;
            this.produto.SKU = produtoAlterado.SKU;
            this.produto.Descricao = produtoAlterado.Descricao;
            this.produto.Preco = produtoAlterado.Preco;
        }
    }

    public class AtributosProduto
    {
        public int Id { get; set; }
        public string? SKU { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
    }

    
}
