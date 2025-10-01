namespace LojaVirtual.Web.Models
{
    public class LoginViewModel
    {
        public string? Email { get; set; }
        public string? PassWord { get; set; }
        //configurar o lembrar me futuramente no bakend
        public bool LembrarMe { get; set; }
    }
}
