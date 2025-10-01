using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string? PrimeiroNome { get; set; }
        [Required]
        public string? UltimoNome { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "As senhas não coincidem.")]
        public string? ConfirmPassword { get; set; }
    }
}
