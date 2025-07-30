using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.CatalogoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcasController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> GetMarcas()
        {
            var marcas = await _marcaService.GetMarcas();
            if (!marcas.Any())
            {
                return NotFound("Nenhuma marca encontrada.");
            }

            return Ok(marcas);
        }

        [HttpPost]
        public async Task<ActionResult<MarcaDTO>> AddMarca([FromBody] MarcaDTO marcaDTO)
        {
            var marcaExistente = await _marcaService.GetMarca(marcaDTO.Handle);

            if (marcaExistente is not null)
            {
                return BadRequest($"Marca {marcaExistente.Nome} já existe.");
            }
            if (string.IsNullOrWhiteSpace(marcaDTO.Nome))
            {
                return BadRequest("O nome da marca não pode ser vazio.");
            }

            await _marcaService.AddMarca(marcaDTO);

            return Ok(marcaDTO);
        }

        [HttpPut]
        public async Task<ActionResult<MarcaDTO>> UpdateMarca([FromBody] MarcaDTO marcaDTO)
        {
            if (marcaDTO is null)
            {
                return BadRequest("Dados da marca inválidos.");
            }

            if (string.IsNullOrWhiteSpace(marcaDTO.Nome))
            {
                return BadRequest("O nome da marca não pode ser vazio.");
            }

            await _marcaService.UpdateMarca(marcaDTO);

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteMarca(long handle)
        {
            var marcaDTO = await _marcaService.GetMarca(handle);

            if (marcaDTO is null)
            {
                return NotFound($"Marca com id {handle} não encontrada.");
            }
            bool existeProdutosVinculados = await _marcaService.ExistProdutosByMarcas(handle);

            if (existeProdutosVinculados)
            {
                return BadRequest("Não é possível excluir uma marca que ainda possui produtos cadastrados a ela.");
            }

            marcaDTO = await _marcaService.DeleteMarca(handle);

            return Ok(marcaDTO);
        }

        [HttpGet("{handle}")]
        public async Task<ActionResult<MarcaDTO>> GetMarca(long handle)
        {
            var marca = await _marcaService.GetMarca(handle);
            if (marca is null)
            {
                return NotFound($"Marca com ID {handle} não encontrada.");
            }

            return Ok(marca);
        }
    }
}
