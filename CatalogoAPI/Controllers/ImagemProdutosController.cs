using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.CatalogoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagemProdutosController : ControllerBase
    {
        private readonly IImagemProdutoService _imagemProdutoService;
        private readonly IProdutoService _produtoService;

        public ImagemProdutosController(IImagemProdutoService imagemProdutoService, IProdutoService produtoService)
        {
            _imagemProdutoService = imagemProdutoService;
            _produtoService = produtoService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddImagemProdutos([FromForm] FileUploadDTO fileUploadDTO, [FromQuery] long produtoHandle)
        {
            var produto = await _produtoService.GetProduto(produtoHandle);
            if (produto == null)
            {
                return NotFound("Produto não encontrado.");
            }
            if (fileUploadDTO.File is null)
            {
                return BadRequest("Nenhuma imagem enviada.");
            }

            await _imagemProdutoService.AddImagemProduto(fileUploadDTO, produto);

            return Ok();
        }

        [HttpGet("{produtoHandle}")]
        public async Task<IActionResult> GetImagensPorProdutoId(long produtoHandle)
        {
            var imagens = await _imagemProdutoService.GetImagensProdutoByProdutoHandle(produtoHandle);
            if (imagens is null || !imagens.Any())
            {
                return NotFound("Nenhuma imagem encontrada para o produto.");
            }

            return Ok(imagens);
        }

        [HttpDelete("{imagemHandle}")]
        public async Task<IActionResult> DeleteImagemProduto(long imagemHandle)
        {
            if (imagemHandle <= 0)
            {
                return BadRequest("Handle inválido.");
            }

            var imagem = await _imagemProdutoService.DeleteImagemProduto(imagemHandle);
            if (imagem is null)
            {
                return NotFound("Erro ao tentar deletar imagem");
            }

            var imagensRestantes = await _imagemProdutoService.GetImagensProdutoByProdutoHandle(imagem.ProdutoId);

            if (imagensRestantes is null)
            {
                return NotFound("Não foi possível carregar as imagens novamente.");
            }

            return Ok(imagensRestantes);
        }
    }
}
