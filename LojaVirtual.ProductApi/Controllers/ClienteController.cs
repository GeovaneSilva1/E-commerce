using LojaVirtual.ProductApi.Classes;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation(Summary = "Cadastrar novo cliente", Description = "Cadastra um novo cliente ao sistema.")]
        public IActionResult AddCliente([FromBody] ClienteRequest clienteRequest)
        {
            var cliente = new Cliente(clienteRequest.CNPJ, clienteRequest.RazaoSocial);

            _clienteRepository.Add(cliente);
            
            return Ok(cliente);
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [Route("ObterClientes")]
        [HttpGet]
        [SwaggerOperation(Summary = "Listar todos os clientes", Description = "Retorna todos os clientes ativos no sistema.")]
        public IActionResult GetCliente()
        {
            var cliente = _clienteRepository.Get();

            if (cliente is null)
            {
                return BadRequest("Nenhum cliente cadastrado!");
            }

            ClienteResponse clienteResponse = new ClienteResponse();
            clienteResponse.IncluirAtributos(cliente);
            return Ok(clienteResponse);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Listar cliente", Description = "Retorna um cliente específico pelo Id.")]
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
        [SwaggerOperation(Summary = "Alterar cliente", Description = "Altera um cliente específico pelo Id.")]
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
        [SwaggerOperation(Summary = "Deletar cliente", Description = "Apaga um cliente específico pelo Id.")]
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
