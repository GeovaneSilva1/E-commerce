using LojaVirtual.Web.DTOs;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models.Compostas
{
    public class ProdutoImagensViewModel
    {
        public long HandleProduto { get; set; }
        public string? NomeProduto { get; set; }
        public IEnumerable<ImagemProdutoViewModel>? ImagemProdutos { get; set; }
        [Required(ErrorMessage = "Necessário adicionar ao menos uma imagem")]
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
