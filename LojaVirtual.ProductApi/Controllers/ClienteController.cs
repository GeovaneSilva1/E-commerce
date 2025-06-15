using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;
using LojaVirtual.ProductApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace LojaVirtual.ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        

        public ClienteController(IClienteRepository clienteRepository, IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [Route("CadastrarCliente")]
        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar novo cliente", Description = "Cadastra um novo cliente ao sistema.")]
        public async Task<ActionResult<ClienteDTO>> AddCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO is null)
            {
                return BadRequest("Dados de cliente Inválidos");
            }

            var clienteEncontrado = await _clienteService.GetClienteById(clienteDTO.Id);
            if (clienteEncontrado is not null)
            {
                return BadRequest("Cliente Existente!");
            }

            await _clienteService.AddCliente(clienteDTO);

            return new CreatedAtRouteResult("GetCliente", new { id = clienteDTO.Id}, clienteDTO);
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        [Route("ObterClientes")]
        [HttpGet]
        [SwaggerOperation(Summary = "Listar todos os clientes", Description = "Retorna todos os clientes ativos no sistema.")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetCliente()
        {
            var clienteDTO = await _clienteService.GetClientes();
            if (clienteDTO is null)
                return NotFound("Nenhum cliente encontrado!");
            
            return Ok(clienteDTO);
        }

        [HttpGet("{id:int}", Name = "GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(int id)
        {
            var clienteDTO = await _clienteService.GetClienteById(id);
            if (clienteDTO is null)
            {
                return BadRequest("Cliente não encontrado");
            }
            return Ok(clienteDTO);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Alterar cliente", Description = "Altera um cliente específico pelo Id.")]
        public async Task<ActionResult<ClienteDTO>> EditCliente(int id, [FromBody] ClienteDTO clienteDTO)
        {
            var clienteEncontrado = await _clienteService.ExistCLienteById(id);
            if (!clienteEncontrado)
            {
                return BadRequest("Cliente não encontrado");
            }

            clienteDTO.Id = id;

            await _clienteService.UpdateClienteById(clienteDTO);
            
            return Ok(clienteDTO);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Deletar cliente", Description = "Apaga um cliente específico pelo Id.")]
        public async Task<ActionResult<ClienteDTO>> DeleteCliente(int id)
        {
            bool clienteEncontrado = await _clienteService.ExistCLienteById(id);

            if (!clienteEncontrado)
            {
                return NotFound("Cliente não encontrado.");
            }

            var clienteDTO = await _clienteService.RemoveCliente(id);
            
            return Ok(clienteDTO);
        }

    }
}
