namespace LojaVirtual.ProductApi.DTOs
{
    public class VendaItemResponseDTO
    {
        public IEnumerable<VendaItemDTO>? VendaItensDTO { get; set; }
        public decimal? valorTotalCompra { get; set; }
    }
}
