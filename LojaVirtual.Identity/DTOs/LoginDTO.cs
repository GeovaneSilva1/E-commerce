namespace LojaVirtual.Identity.DTOs
{
    public class LoginDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public bool LembrarMe { get; set; }
        public string? Token { get;  set; }
        public string? PrimeiroNome { get; set; }
    }
}
