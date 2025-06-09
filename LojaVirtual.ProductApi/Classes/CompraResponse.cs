namespace LojaVirtual.ProductApi.Classes
{
    public class CompraResponse: Compra
    {
        public decimal ValorTotalVenda { get; set; }

        public CompraResponse(Compra compra, decimal ValorTotalVenda)
        {
            this.CpfCnpj = compra.CpfCnpj;
            this.NomeRazao = compra.NomeRazao;
            this.CondicaoPagamento = compra.CondicaoPagamento;
            this.Itens = compra.Itens;
            this.Email = compra.Email;

            this.ValorTotalVenda = ValorTotalVenda;
        }
    }

    
}
