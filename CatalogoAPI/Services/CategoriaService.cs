using AutoMapper;
using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using LojaVirtual.CatalogoAPI.Services.Interfaces;
using System.Reflection.Metadata;

namespace LojaVirtual.CatalogoAPI.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        public async Task AddCategoria(CategoriaDTO categoriaDTO)
        {
            categoriaDTO.Handle = 0;
            Categoria categoria = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepository.Add(categoria);
        }

        public async Task<CategoriaDTO> DeleteCategoria(long handle)
        {
            Categoria categoria = await _categoriaRepository.Delete(handle);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<bool> ExistProdutosByCategoria(long handle)
        {
            return await _categoriaRepository.ExistsProdutosByCategoria(handle);
        }

        public async Task<CategoriaDTO> GetCategoria(long handle)
        {
            Categoria categoria = await _categoriaRepository.Get(handle);
            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<IEnumerable<CategoriaDTO>> GetCategorias()
        {
            var categorias = await _categoriaRepository.GetMany();
            return _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
        }

        public async Task UpdateCategoria(CategoriaDTO categoriaDTO)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDTO);
            await _categoriaRepository.Update(categoria);
        }
    }
}
