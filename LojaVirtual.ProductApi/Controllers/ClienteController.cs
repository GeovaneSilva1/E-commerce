using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/v1/cliente")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [Route("CadastrarCliente")]
        [HttpPost]
        public IActionResult AddCliente([FromBody] ClienteRequest clienteRequest)
        {
            var cliente = new Cliente(clienteRequest.CNPJ, clienteRequest.RazaoSocial);

            _clienteRepository.Add(cliente);
            
            return Ok(cliente);
        }

        [Route("ObterClientes")]
        [HttpGet]
        public IActionResult GetCliente()
        {
            var cliente = _clienteRepository.Get();

            ClienteResponse clienteResponse = new ClienteResponse();
            clienteResponse.IncluirAtributos(cliente);
            return Ok(clienteResponse);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetClienteById(int id)
        {
            var clienteSolicitado = _clienteRepository.ExistById(id);
            if (!clienteSolicitado)
                return NotFound("Cliente não encontrado.");

            var cliente = _clienteRepository.GetById(id);
            
            ClienteResponse clienteResponse = new ClienteResponse();
            clienteResponse.IncluirAtributos(cliente);
            return Ok(clienteResponse);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditCliente(int id, [FromBody] ClienteRequest clienteRequest)
        {
            var clienteSolicitado = _clienteRepository.ExistById(id);

            if (!clienteSolicitado)
               return NotFound("Cliente não encontrado.");

            Cliente cliente = new Cliente(clienteRequest.CNPJ, clienteRequest.RazaoSocial);

            cliente.Id = id;

            _clienteRepository.Update(cliente);

            return Ok(cliente);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCliente(int id)
        {
            var clienteSolicitado = _clienteRepository.ExistById(id);

            if (!clienteSolicitado)
                return NotFound("Cliente não encontrado.");

            var clienteDeletado = _clienteRepository.DeleteById(id);

            return Ok(clienteDeletado);
        }

    }
}
