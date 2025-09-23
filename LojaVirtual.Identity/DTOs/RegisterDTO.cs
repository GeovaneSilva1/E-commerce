namespace LojaVirtual.Identity.DTOs
{
    public class RegisterDTO
    {
        public string? PrimeiroNome { get; set; }
        public string? UltimoNome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? ConfirmacaoSenha { get; set; }
        public string? Token { get; set; }
        public string? UserName { get; set; }
    }
}
