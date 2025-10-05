using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? PassWord { get; set; }

        //configurar o lembrar me futuramente no bakend
        [Display(Name = "Lembrar-me")]
        public bool LembrarMe { get; set; }
    }
}
