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
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping() {

            CreateMap<PaisDto, Pais>();
            CreateMap<PaisDto, IEnumerable<Pais>>();

            CreateMap<CreatePaisDto, Pais>();
            CreateMap<UpdatePaisDto, Pais>();

            CreateMap<DestinoDto, Destino>();
            CreateMap<CreateDestinoDto, Destino>();
            CreateMap<UpdateDestinoDto, Destino>();

            CreateMap<CreateTourDto, Tour>();
            CreateMap<UpdateTourDto, Tour>();



        }
    }
}
