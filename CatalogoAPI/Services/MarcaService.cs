using AutoMapper;
using LojaVirtual.CatalogoAPI.DTOs;
using LojaVirtual.CatalogoAPI.Infraestrutura;
using LojaVirtual.CatalogoAPI.Infraestrutura.Interfaces;
using LojaVirtual.CatalogoAPI.Models;
using LojaVirtual.CatalogoAPI.Services.Interfaces;

namespace LojaVirtual.CatalogoAPI.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;
        private readonly IMapper _mapper;

        public MarcaService(IMarcaRepository marcaRepository, IMapper mapper)
        {
            _marcaRepository = marcaRepository;
            _mapper = mapper;
        }

        public async Task AddMarca(MarcaDTO marcaDTO)
        {
            marcaDTO.Handle = 0;
            Marca marca = _mapper.Map<Marca>(marcaDTO);
            await _marcaRepository.Add(marca);
        }

        public async Task<MarcaDTO> DeleteMarca(long handle)
        {
            Marca marca = await _marcaRepository.Delete(handle);
            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<bool> ExistProdutosByMarcas(long handle)
        {
            return await _marcaRepository.ExistsProdutosByMarca(handle);
        }

        public async Task<MarcaDTO> GetMarca(long handle)
        {
            Marca marca = await _marcaRepository.Get(handle);
            return _mapper.Map<MarcaDTO>(marca);
        }

        public async Task<IEnumerable<MarcaDTO>> GetMarcas()
        {
            var marcas = await _marcaRepository.GetMany();
            return _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
        }

        public async Task UpdateMarca(MarcaDTO marcaDTO)
        {
            var marca = _mapper.Map<Marca>(marcaDTO);
            await _marcaRepository.Update(marca);
        }
    }
}
