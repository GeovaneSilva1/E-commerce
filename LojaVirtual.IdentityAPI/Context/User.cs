using Microsoft.AspNetCore.Identity;

namespace LojaVirtual.IdentityAPI.Context
{
    public class User : IdentityUser
    {
        public string? PrimeiroNome { get; set; }
        public string? UltimoNome { get; set; }
    }
}
