using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string? PrimeiroNome { get; set; }
        
        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [Display(Name = "Sobrenome")]
        public string? UltimoNome { get; set; }
        
        [Required(ErrorMessage = "O email é obrigatório")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "A senha é obrigatória")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "A confirmação é obrigatória")]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
