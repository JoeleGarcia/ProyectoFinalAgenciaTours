using AutoMapper;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class DestinoService : IDestinoService
    {

        private readonly IDestinoRepository _DestinosRepository;
        private readonly IMapper _mapper;

        public DestinoService(IDestinoRepository destinoRepository, IMapper mapper)
        {
            _DestinosRepository = destinoRepository;
            _mapper = mapper;
        }
        public async Task<CreateDestinoDto> AddDestinoAsync(CreateDestinoDto createDestinoDto)
        {
            try
            {
                var _destino = _mapper.Map<Destino>(createDestinoDto);
                var destinoAgregado = await _DestinosRepository.AddDestinosAsync(_destino);

                var createDestino = _mapper.Map<CreateDestinoDto>(destinoAgregado);

                return createDestino;

            }
            catch (InfrastructureException ex)
            {
                throw new ApplicationException("No se pudo registrar el usuario. Intente más tarde.", ex);
            }
        }

        public async Task DeleteDestinoAsync(int id)
        {
            try
            {
                await _DestinosRepository.DeleteDestinosAsync(id);
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

        public async Task<IEnumerable<DestinoDto>> GetAllDestinoAsync()
        {
            var destinos = await _DestinosRepository.GetAllDestinosAsync();
            return _mapper.Map<IEnumerable<DestinoDto>>(destinos);
        }

        public async Task<DestinoDto> GetDestinoByIdAsync(int id)
        {
            var _destino = await _DestinosRepository.GetDestinosByIdAsync(id);
            return _mapper.Map<DestinoDto>(_destino);
        }

        public async Task UpdateDestinoAsync(int id, UpdateDestinoDto updateDestinoDto)
        {
            var _destino = await _DestinosRepository.GetDestinosByIdAsync(id);

            if (_destino is null)
            {
                throw new NotFoundException("Pais", id);
            }

            var updatedDestino = _mapper.Map<Destino>(updateDestinoDto);
            await _DestinosRepository.UpdateDestinosAsync(updatedDestino);
        }
    }
}
