using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.Web.Models
{
    public class CategoriaViewModel
    {
        public long Handle { get; set; }

        [Required(ErrorMessage = "O Nome da categoria é obrigátório!")]
        public string? Nome { get; set; }
    }
}
