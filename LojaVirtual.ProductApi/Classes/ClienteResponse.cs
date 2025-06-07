using LojaVirtual.ProductApi.Models;
using System.Diagnostics.Metrics;

namespace LojaVirtual.ProductApi.Classes
{
    public class ClienteResponse
    {
        public List<AtributosClientes>? clienteResponses { get; set; }
        public AtributosClientes? clienteResponse { get; set; }


        public void IncluirAtributos(List<Cliente> clientes)
        {
            this.clienteResponses = new List<AtributosClientes>();

            for (int count = 0; count < clientes.Count; count++)
            {
                this.clienteResponses.Add(new AtributosClientes
                {
                    Id = clientes[count].Id,
                    CNPJ = clientes[count].CNPJ,
                    RazaoSocial = clientes[count].RazaoSocial
                }); 
            }
        }

        public void IncluirAtributos(Cliente cliente)
        {
            this.clienteResponse = new AtributosClientes();

            this.clienteResponse.Id = cliente.Id;
            this.clienteResponse.CNPJ = cliente.CNPJ;
            this.clienteResponse.RazaoSocial = cliente.RazaoSocial;
        }
    }
    public class AtributosClientes
    {
        public int Id { get; set; }
        public string? CNPJ { get; set; }
        public string? RazaoSocial { get; set; }
    }
}
