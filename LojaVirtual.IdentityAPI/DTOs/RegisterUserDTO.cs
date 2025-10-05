namespace LojaVirtual.IdentityAPI.DTOs
{
    public class RegisterUserDTO
    {
        public string? PrimeiroNome { get; set; }
        public string? UltimoNome { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
