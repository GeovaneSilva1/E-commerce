using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LojaVirtual.Web.Models
{
    public class MarcaViewModel
    {
        public long Handle { get; set; }
        [Required(ErrorMessage = "O Nome da marca é obrigátório!")]
        public string? Nome { get; set; }
    }
}
