using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class MarcaRepository : IMarcaRepository
    {
        public Task Add(Marca marca)
        {
            throw new NotImplementedException();
        }

        public Task<Marca> Get(long handle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Marca>> GetMany()
        {
            throw new NotImplementedException();
        }
    }
}
