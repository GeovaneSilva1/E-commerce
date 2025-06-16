using AutoMapper;
using LojaVirtual.ProductApi.Context;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.ProductApi.Infraestrutura
{
    public class TabelaPrecoRepository : ITabelaPrecoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public TabelaPrecoRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task Add(TabelaPreco tabelaPreco)
        {
            _appDbContext.TabelaPrecos.Add(tabelaPreco);
            await _appDbContext.SaveChangesAsync();
        }

        public List<TabelaPreco> GetMany()
        {
            return _appDbContext.TabelaPrecos.ToList();
        }

        public async Task<IEnumerable<TabelaPreco>> GetManyAsync()
        {
            return await _appDbContext.TabelaPrecos.ToListAsync();
        }

        public async Task<TabelaPrecoDTO> ValidaByDescricaoAsync(string descricaoTabelaPreco)
        {
            var tabelaPreco = await _appDbContext.TabelaPrecos.Where(tp => tp.DataFim >= DateTime.Now && tp.Descricao.Contains(descricaoTabelaPreco)).FirstOrDefaultAsync();
            return _mapper.Map<TabelaPrecoDTO>(tabelaPreco);
        }
    }
}
