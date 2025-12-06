using AutoMapper;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Mapping
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping() {

            CreateMap<Pais, PaisDto>();
            CreateMap<Pais, IEnumerable<PaisDto>>();
            CreateMap<Pais, CreatePaisDto>();
            CreateMap<Pais, UpdatePaisDto>();

        }
    }
}
