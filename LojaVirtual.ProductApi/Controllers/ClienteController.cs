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
        public IActionResult AddCliente(int id, string cnpj, string razaoSocial)
        {
            var cliente = new Cliente(id, cnpj,razaoSocial);
            _clienteRepository.Add(cliente);
            
            return Ok(cliente);
        }

        [Route("ObterClientes")]
        [HttpGet]
        public IActionResult GetCliente()
        {
            var cliente = _clienteRepository.Get();

            return Ok(cliente);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetClienteById(int id)
        {
            var clienteSolicitado = _clienteRepository.ExistById(id);
            if (!clienteSolicitado)
                return NotFound("Cliente não encontrado.");

            var cliente = _clienteRepository.GetById(id);

            return Ok(cliente);
        }

        [HttpPut("{id:int}")]
        public IActionResult EditCliente(int id, [FromBody] Cliente cliente)
        {
            var clienteSolicitado = _clienteRepository.ExistById(id);

            if (!clienteSolicitado)
               return NotFound("Cliente não encontrado.");
            
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
