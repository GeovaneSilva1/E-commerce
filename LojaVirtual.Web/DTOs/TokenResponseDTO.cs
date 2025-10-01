namespace LojaVirtual.Web.DTOs
{
    public class TokenResponseDTO
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
