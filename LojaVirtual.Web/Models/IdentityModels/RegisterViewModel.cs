using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models.IdentityModels
{
    public class RegisterViewModel
    {
        [Required]
        public string? PrimeiroNome { get; set; }
        
        [Required]
        public string? UltimoNome { get; set; }
        
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de Senha")]
        [Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string? ConfirmacaoSenha { get; set; }
    }
}
