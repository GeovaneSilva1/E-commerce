using AutoMapper;
using LojaVirtual.CatalogoAPI.Context;
using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContextCatalogoApi _contextCatalogo;
        private readonly IMapper _mapper;

        public CategoriaRepository(AppDbContextCatalogoApi contextCatalogo, IMapper mapper)
        {
            _contextCatalogo = contextCatalogo;
            _mapper = mapper;
        }

        public async Task Add(Categoria categoria)
        {
            _contextCatalogo.Categorias.Add(categoria);
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task<Categoria> Delete(long handle)
        {
            var categoria = await Get(handle);
            _contextCatalogo.Categorias.Remove(categoria);
            await _contextCatalogo.SaveChangesAsync();
            return categoria;
        }

        public async Task<Categoria> Get(long handle)
        {
            return await _contextCatalogo.Categorias.Where(p => p.Handle == handle).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Categoria>> GetMany()
        {
            return await _contextCatalogo.Categorias.ToListAsync();
        }

        public async Task<bool> ExistsProdutosByCategoria(long handle)
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Where(p => p.CategoriaId == handle)
                .AnyAsync();
        }

        public Task Update(Categoria categoria)
        {
            _contextCatalogo.Entry(categoria).State = EntityState.Modified;
            return _contextCatalogo.SaveChangesAsync();
        }

        public async Task<CategoriaDTO> ValidaByNome(string nomeCategoria)
        {
            var categoria = await _contextCatalogo.Categorias.Where(tp => tp.Nome.Contains(nomeCategoria)).FirstOrDefaultAsync();
            return _mapper.Map<CategoriaDTO>(categoria);
        }
    }
}
