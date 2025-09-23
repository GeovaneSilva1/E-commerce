using Microsoft.AspNetCore.Identity;

namespace LojaVirtual.Identity.Context
{
    public class ApplicationUser : IdentityUser
    {
        public string? PrimeiroNome { get; set; }
        public string? UltimoNome { get; set; }
    }
}
