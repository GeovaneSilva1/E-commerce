using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.DTOs
{
    public class FileUploadDTO
    {
        [Required(ErrorMessage = "Necessário adicionar ao menos uma imagem")]
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
