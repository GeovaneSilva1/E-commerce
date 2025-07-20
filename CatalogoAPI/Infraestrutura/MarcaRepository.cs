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

        public async Task<Marca> Get(long handle)
        {
            return await _contextCatalogo.Marcas.Where(p => p.Handle == handle).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Marca>> GetMany()
        {
            return await _contextCatalogo.Marcas.ToListAsync();
        }
    }
}
