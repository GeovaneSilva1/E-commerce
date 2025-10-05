using AutoMapper;
using LojaVirtual.CatalogoAPI.Context;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContextCatalogoApi _contextCatalogo;
        private readonly IMapper _mapper;

        public MarcaRepository(AppDbContextCatalogoApi contextCatalogo, IMapper mapper)
        {
            _contextCatalogo = contextCatalogo;
            _mapper = mapper;
        }

        public async Task Add(Marca marca)
        {
            _contextCatalogo.Marcas.Add(marca);
            await _contextCatalogo.SaveChangesAsync();
        }

        public async Task<Marca> Delete(long handle)
        {
            Marca marca = await Get(handle);
            _contextCatalogo.Marcas.Remove(marca);
            await _contextCatalogo.SaveChangesAsync();
            return marca;
        }

        public async Task<bool> ExistsProdutosByMarca(long handle)
        {
            return await _contextCatalogo.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Marca)
                .Where(p => p.MarcaId == handle)
                .AnyAsync();
        }

        public async Task<Marca> Get(long handle)
        {
            return await _contextCatalogo.Marcas.Where(p => p.Handle == handle).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Marca>> GetMany()
        {
            return await _contextCatalogo.Marcas.ToListAsync();
        }

        public async Task Update(Marca marca)
        {
            _contextCatalogo.Entry(marca).State = EntityState.Modified;
            await _contextCatalogo.SaveChangesAsync();
        }
    }
}
