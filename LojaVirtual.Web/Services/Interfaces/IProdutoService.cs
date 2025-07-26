using LojaVirtual.Web.Models;

namespace LojaVirtual.Web.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> ObterProdutosAsync();
        Task<ProdutoViewModel> ObterProdutoPorIdAsync(long handle);
        Task<ProdutoViewModel> CriarProdutoAsync(ProdutoViewModel produtoVM);
        Task<ProdutoViewModel> AtualizarProdutoAsync(ProdutoViewModel produtoVM);
        Task<bool> DeletarProdutoAsync(int id);

    }
}
