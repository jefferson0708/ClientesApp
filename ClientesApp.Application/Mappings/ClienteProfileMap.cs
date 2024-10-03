using AutoMapper;
using ClienteApp.Domain.Entities;
using ClientesApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Mappings
{
    public class ClienteProfileMap: Profile
    {
        public ClienteProfileMap()
        {
            CreateMap<ClienteRequestDto, Cliente>();
            CreateMap<Cliente, ClienteResponseDto>();
        }
    }
}
