using AutoMapper;
using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Mapping
{
    public class DtoToDtoMapping : Profile
    {

        public DtoToDtoMapping()
        {

            CreateMap<PaisDto, UpdatePaisDto>();

        }

    }
}
