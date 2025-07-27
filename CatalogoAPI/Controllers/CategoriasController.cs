using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace LojaVirtual.CatalogoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {   
        private readonly ICategoriaService _categoriaService;
        public CategoriasController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias = await _categoriaService.GetCategorias();
            if (!categorias.Any())
            {
                return NotFound("Nenhuma categoria encontrada.");
            }
            return Ok(categorias);
        }

        [HttpPost]
        public async Task<ActionResult<CategoriaDTO>> AddCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            var categoriaExistente = await _categoriaService.GetCategoria(categoriaDTO.Handle);
            
            if (categoriaExistente is not null)
            {
                return BadRequest("Categoria já existe.");
            }
            if (string.IsNullOrWhiteSpace(categoriaDTO.Nome))
            {
                return BadRequest("O nome da categoria não pode ser vazio.");
            }

            await _categoriaService.AddCategoria(categoriaDTO);
            
            return Ok(categoriaDTO);
        }
        
        [HttpPut]
        public async Task<ActionResult<CategoriaDTO>> UpdateCategoria([FromBody] CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
            {
                return BadRequest("Dados da categoria inválidos.");
            }

            if (string.IsNullOrWhiteSpace(categoriaDTO.Nome))
            {
                return BadRequest("O nome da categoria não pode ser vazio.");
            }

            await _categoriaService.UpdateCategoria(categoriaDTO);
            
            return Ok(categoriaDTO);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCategoria(long handle)
        { 
            var categoriaDTO = await _categoriaService.GetCategoria(handle);

            if (categoriaDTO is null)
            {
                return NotFound($"Categoria com id {handle} não encontrada.");
            }
            bool existeProdutosVinculados = await _categoriaService.ExistProdutosByCategoria(handle);

            if (existeProdutosVinculados)
            {
                return BadRequest("Não é possível excluir uma categoria que ainda possui produtos cadastrados a ela.");
            }

            categoriaDTO = await _categoriaService.DeleteCategoria(handle);

            return Ok(categoriaDTO);
        }

        [HttpGet("{handle}")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(long handle)
        {
            var categoria = await _categoriaService.GetCategoria(handle);
            if (categoria is null)
            {
                return NotFound($"Categoria com ID {handle} não encontrada.");
            }
            return Ok(categoria);
        }
    }
}
