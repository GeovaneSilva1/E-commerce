using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;

namespace LojaVirtual.CatalogoAPI.Infraestrutura
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Task Add(Categoria categoria)
        {
            throw new NotImplementedException();
        }

        public Task<Categoria> Get(long handle)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Categoria>> GetMany()
        {
            throw new NotImplementedException();
        }

        public Task<CategoriaDTO> ValidaByNome(string nomeCategoria)
        {
            throw new NotImplementedException();
        }
    }
}
