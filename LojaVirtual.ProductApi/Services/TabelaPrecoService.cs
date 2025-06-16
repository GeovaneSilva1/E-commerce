using AutoMapper;
using LojaVirtual.ProductApi.DTOs;
using LojaVirtual.ProductApi.Infraestrutura;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.Services
{
    public class TabelaPrecoService : ITabelaPrecoService
    {
        private readonly ITabelaPrecoRepository _tabelaPrecoRepository;
        private readonly IMapper _mapper;

        public TabelaPrecoService(ITabelaPrecoRepository tabelaPrecoRepository, IMapper mapper)
        {
            _tabelaPrecoRepository = tabelaPrecoRepository;
            _mapper = mapper;
        }

        public async Task AddTabelaPreco(TabelaPrecoDTO tabelaPrecoDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TabelaPrecoResponseDTO>> RetornaTabelaPrecosInseridas(IEnumerable<TabelaPrecoDTO> tabelaPrecoDTOs)
        {
            List<TabelaPreco> tabelaPrecosInseridas = new List<TabelaPreco>();

            foreach (var tabelaPrecoDTO in tabelaPrecoDTOs)
            {
                tabelaPrecoDTO.DataInicio = DateTime.Now;
                tabelaPrecoDTO.DataFim = RetornaDataByDias(tabelaPrecoDTO.DiasValidos);
                TabelaPreco tabelaPreco = _mapper.Map<TabelaPreco>(tabelaPrecoDTO);

                await _tabelaPrecoRepository.Add(tabelaPreco);

                tabelaPrecosInseridas.Add(tabelaPreco);
            }

            return _mapper.Map<IEnumerable<TabelaPrecoResponseDTO>>(tabelaPrecosInseridas);
        }

        public async Task<TabelaPrecoDTO> ValidaByDescricao(string descricaoTabelaPreco)
        {
            var tabelaprecoDTO = await _tabelaPrecoRepository.ValidaByDescricaoAsync(descricaoTabelaPreco);
            return tabelaprecoDTO;
        }

        public async Task<IEnumerable<TabelaPrecoResponseDTO>> GetTabelaPrecos()
        {
            IEnumerable<TabelaPreco> tabelaPreco = await _tabelaPrecoRepository.GetManyAsync();
            return _mapper.Map<IEnumerable<TabelaPrecoResponseDTO>>(tabelaPreco);
        }

        private DateTime RetornaDataByDias(int dias)
        {
            DateTime DataFim = DateTime.Now.AddDays(dias);
            return DataFim;
        }

        public TabelaPreco RetornaInstanciaByDTO(TabelaPrecoDTO tabelaPrecoDTO)
        {
            return _mapper.Map<TabelaPreco>(tabelaPrecoDTO);
        }
    }
}
