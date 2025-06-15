using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LojaVirtual.ProductApi.Models;

namespace LojaVirtual.ProductApi.DTOs.Mapeamentos
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<ClienteDTO, Cliente>();
            CreateMap<Cliente, ClienteDTO>();
        }
    }
}
