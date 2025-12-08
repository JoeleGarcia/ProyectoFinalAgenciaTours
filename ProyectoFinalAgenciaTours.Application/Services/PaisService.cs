using AutoMapper;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisRepository;
        private readonly IMapper _mapper;

        public PaisService(IPaisRepository paisRepository, IMapper mapper)
        {
            _paisRepository = paisRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaisDto> AddPaisAsync(CreatePaisDto createPaisDto)
        {
            try
            {
                var _pais = _mapper.Map<Pais>(createPaisDto);
                var paisAgregado = await _paisRepository.AddPaisAsync(_pais);

                var CreatePaisDto = _mapper.Map<CreatePaisDto>(paisAgregado);
                
                return CreatePaisDto;

            }
            catch (InfrastructureException ex)
            {
                throw new ApplicationException("No se pudo registrar el usuario. Intente más tarde.", ex);
            }
        }

        public async Task DeletePaisAsync(int id)
        {
            try
            {
                await _paisRepository.DeletePaisAsync(id);
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Timeout al consultar la base de datos", ex);

            }
            catch (Exception ex)
            {
                throw new NotFoundException("Pais", ex.Message);
            }
        }

        public async Task<IEnumerable<PaisDto>> GetAllPaisAsync()
        {
            var paises = await _paisRepository.GetAllPaisAsync();
            return _mapper.Map<IEnumerable<PaisDto>>(paises);
        }

        public async Task<PaisDto> GetPaisByIdAsync(int id)
        {
            var _pais = await _paisRepository.GetPaisByIdAsync(id);
            return _mapper.Map<PaisDto>(_pais);
        }

        public async Task UpdatePaisAsync(int id, UpdatePaisDto updatePaisDto)
        {
            var _pais = await _paisRepository.GetPaisByIdAsync(id);

            if (_pais is null)
            {
                throw new NotFoundException("Pais", id);
            }

            var updatedPais = _mapper.Map<Pais>(updatePaisDto);        

            await _paisRepository.UpdatePaisAsync(updatedPais);
        }
    }
}
