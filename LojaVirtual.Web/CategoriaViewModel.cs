using System.Text.Json.Serialization;

namespace LojaVirtual.Web
{
    public class CategoriaViewModel
    {
        [JsonIgnore]
        public long Handle { get; set; }
        public string? Nome { get; set; }
    }
}
